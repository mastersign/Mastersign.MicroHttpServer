param (
    $ConfigFile = "$(Split-Path $MyInvocation.MyCommand.Definition -Parent)\configs\default.json",
    $ProjectFile = "$(Split-Path $MyInvocation.MyCommand.Definition -Parent)\..\Mastersign.MicroHttpServer.Benchmark\Mastersign.MicroHttpServer.Benchmark.csproj",
    [switch]$NoBuild,
    [switch]$NoServerStart,
    [switch]$NoWarmUp,
    [switch]$GridView
)
$config = Get-Content $ConfigFile | ConvertFrom-Json

$thisDir = Split-Path $MyInvocation.MyCommand.Definition -Parent
$resultBaseDir = "$thisDir\results"

$ErrorActionPreference = "Stop"

if (!(Test-Path $resultBaseDir)) { mkdir $resultBaseDir | Out-Null }

function ParseDouble($text, $prefix, $suffix) {
    $p = "^${prefix}:\s+(?<value>\d+(?:\.\d+)?)"
    if ($suffix) { $p += ".*$suffix" }
    $re = New-Object System.Text.RegularExpressions.Regex $p, Multiline
    $m = $re.Match($text)
    if ($m.Success) {
        return [double]::Parse($m.Groups["value"].Value, [Globalization.CultureInfo]::InvariantCulture)
    } else {
        return [double]::NaN
    }
}

function ObjectKeys($o) {
    if (!$o) { return }
    if ($o.Keys) { return $o.Keys }
    if ($o.Properties) { return $o.Properties | ForEach-Object { $_.Name } }
    if ($o.PSObject.Properties) { return $o.PSObject.Properties | ForEach-Object { $_.Name } }
}
function ObjectValue($o, $key) {
    if (!$o) { return }
    if ($o.Keys) { return $o[$key] }
    if ($o.Properties) {
        $prop = $o.Properties | Where-Object "Name" -EQ $key
        return $prop.Value
    }
    if ($o.PSObject.Properties) {
        $prop = $o.PSObject.Properties | Where-Object "Name" -EQ $key
        return $prop.Value
    }
}

Push-Location "$thisDir\.."
try {

    if (!$NoBuild -and !$NoServerStart) {
        foreach ($framework in $config.Frameworks) {
            foreach ($buildConfiguration in $config.Configurations) {
                Write-Host "Building $buildConfiguration for $framework..."
                $dotnetArgs = @(
                    "build"
                    "-f", $framework
                    "-c", $buildConfiguration
                    $ProjectFile
                )
                $buildProc = Start-Process "dotnet" $dotnetArgs -PassThru
                $buildProc.WaitForExit()
                if ($buildProc.ExitCode -ne 0) {
                    Write-Error "Build failed"
                }
            }
        }
    }

    $summary = @()
    foreach ($framework in $config.Frameworks) {
        foreach ($buildConfiguration in $config.Configurations) {
            $resultDir = "$resultBaseDir\$buildConfiguration\$framework"
            if (!(Test-Path $resultDir)) { mkdir $resultDir | Out-Null }

            foreach ($variation in $config.Variations) {
                $fileBase = "$resultDir\$($variation.FileName)"

                Write-Host "Running $($variation.Name)..."
                Write-Host "    Framework:      $framework"
                Write-Host "    Configuration:  $buildConfiguration"
                Write-Host "    Filename        $($variation.FileName)"
                if ($variations.LogToConsole) {
                    Write-Host "    Log Level:      $($variation.LogLevel)"
                    Write-Host "    Log with Color: $($variation.LogWithColor)"
                }

                $serverArgs = @(
                    "run"
                    "--no-restore"
                    "--no-build"
                    "-f", $framework
                    "-c", $buildConfiguration
                    "--project", $ProjectFile
                    "--no-launch-profile"
                    "-Host", $config.Host,
                    "-Port", $config.Port
                )

                if ($variation.LogToConsole) {
                    $serverArgs += "-LogToConsole"
                    if ($variation.LogLevel) {
                        $serverArgs += "-LogLevel"
                        $serverArgs += $variation.LogLevel
                    }
                    if ($variation.LogWithColors) {
                        $serverArgs += "-LogWithColors"
                    }
                }
                if ($variation.NoDelay) {
                    $serverArgs += "-NoDelay"
                }
                if ($variation.Job) {
                    $serverArgs += $variation.Job
                } else {
                    $serverArgs += $variation.Name
                }

                $serverProc = $null
                if (!$NoServerStart) {
                    Write-Host "    Command Line: dotnet $([string]::Join(" ", $serverArgs))"

                    $serverProc = Start-Process "dotnet" $serverArgs -PassThru
                    [Threading.Thread]::Sleep([TimeSpan]::FromSeconds(2))
                    if ($serverProc.HasExited) {
                        Write-Error "Server process has exited, before the benchmark was executed."
                    }
                }

                try {

                    if (!$NoWarmUp) {
                        # Warm up
                        $abArgs = @(
                            "-c", $config.Concurrency
                            "-s", $config.RequestTimeout
                            "-n", $config.WarmUpRequests
                        )
                        if ($config.KeepAlive) { $abArgs += "-k" }
                        foreach ($key in (ObjectKeys $config.Headers)) {
                            $value = ObjectValue $config.Headers $key
                            $abArgs += "-H"
                            $abArgs += "`"${key}: ${value}`""
                        }
                        $abArgs += "http://$($config.Host):$($config.Port)$($variation.Route)"
                        $abProc = Start-Process "ab" $abArgs -PassThru
                        $abProc.WaitForExit()
                        if ($abProc.ExitCode -ne 0) {
                            Write-Error "Apache Benchmark did not exit cleanly. Exit code: $($abProc.ExitCode)"
                        }
                    }

                    # Actual test
                    $abArgs = @(
                        "-c", $config.Concurrency
                        "-s", $config.RequestTimeout
                        "-n", $config.Requests
                        "-g", "${fileBase}.gnuplot"
                        "-e", "${fileBase}.csv"
                    )
                    if ($config.KeepAlive) { $abArgs += "-k" }
                    foreach ($key in (ObjectKeys $config.Headers)) {
                        $value = ObjectValue $config.Headers $key
                        $abArgs += "-H"
                        $abArgs += "`"${key}: ${value}`""
                    }
                    $abArgs += "http://$($config.Host):$($config.Port)$($variation.Route)"
                    Write-Host "    Command Line: ab $([string]::Join(" ", $abArgs))"
                    $abProc = Start-Process "ab" $abArgs -NoNewWindow -RedirectStandardOutput "${fileBase}.txt" -PassThru
                    $abProc.WaitForExit()

                } finally {
                    if ($serverProc -and !$serverProc.HasExited) {
                        $serverProc.Kill()
                        $serverProc.WaitForExit()
                    }
                }

                $abResultText = [IO.File]::ReadAllText("${fileBase}.txt", [Text.Encoding]::UTF8)
                $requestsPerSecond = ParseDouble $abResultText "Requests per second"
                $timePerRquest = ParseDouble $abResultText "Time per request"
                $timePerRequestConcurrent = ParseDouble $abResultText "Time per request" "concurrent"
                $transferRate = ParseDouble $abResultText "Transfer rate"
                $resultInfo = @{
                    "BuildConfiguration" = $buildConfiguration
                    "Framework" = $framework
                    "Variation" = $variation.Name
                    "FileName" = $variation.FileName
                    "Job" = $variation.Job
                    "RequestsPerSecond" = $requestsPerSecond
                    "TimePerRequest" = $timePerRquest
                    "TimePerRequestConcurrent" = $timePerRequestConcurrent
                    "TransferRate" = $transferRate
                }
                $resultObject = New-Object PSObject -Property $resultInfo
                $summary += $resultObject
            }
        }
    }

    $summaryFile = "$resultBaseDir\$([IO.Path]::GetFileNameWithoutExtension($ConfigFile)).csv"
    $summary | ConvertTo-Csv | Out-File $summaryFile -Encoding ASCII

    $sortedSummary = $summary | Sort-Object "RequestsPerSecond" -Descending

    $sortedSummaryFile = "$resultBaseDir\$([IO.Path]::GetFileNameWithoutExtension($ConfigFile))-sorted.csv"
    $sortedSummary | ConvertTo-Csv | Out-File $sortedSummaryFile -Encoding ASCII

    if ($GridView) {
        $sortedSummary | Out-GridView
    } else {
        Write-Host ""
        Write-Host "#### Summary"
        Write-Host ""
        $sortedSummary | Format-Table | Out-Default
    }

} finally {
    Pop-Location
}