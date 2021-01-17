param (
    $ConfigFile = "$(Split-Path $MyInvocation.MyCommand.Definition -Parent)\configs\default.json",
    $ProjectFile = "$(Split-Path $MyInvocation.MyCommand.Definition -Parent)\..\Mastersign.MicroHttpServer.Benchmark\Mastersign.MicroHttpServer.Benchmark.csproj",
    [switch]$NoBuild,
    [switch]$GridView
)
$config = Get-Content $ConfigFile | ConvertFrom-Json

$thisDir = Split-Path $MyInvocation.MyCommand.Definition -Parent
$resultBaseDir = "$thisDir\results"

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

Push-Location "$thisDir\.."
try {

    if (!$NoBuild) {
        foreach ($framework in $config.Frameworks) {
            foreach ($buildConfiguration in $config.Configurations) {
                Write-Host "Building $buildConfiguration for $framework..."
                $dotnetArgs = @(
                    "build"
                    "-f", $framework
                    "-c", $buildConfiguration
                    $ProjectFile
                )
                Start-Process "dotnet" $dotnetArgs -Wait
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
                    "-p", $ProjectFile
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
                if ($variations.Job) {
                    $serverArgs += $variation.Job
                } else {
                    $serverArgs += $variation.Name
                }

                Write-Host "    Command Line: dotnet $([string]::Join(" ", $serverArgs))"

                $serverProc = Start-Process "dotnet" $serverArgs -PassThru
                try {
                    [Threading.Thread]::Sleep([TimeSpan]::FromSeconds(2))

                    $abArgs = @(
                        "-n", $config.Requests
                        "-c", $config.Concurrency
                        "-k"                         # keep alive
                        "-s", "2"                    # timeout for each request in seconds
                        "-g", "${fileBase}.gnuplot"
                        "-e", "${fileBase}.csv"
                        "http://$($config.Host):$($config.Port)$($variation.Route)"
                    )

                    Start-Process "ab" $abArgs -NoNewWindow -Wait -RedirectStandardOutput "${fileBase}.txt"

                } finally {
                    $serverProc.Kill()
                    $serverProc.WaitForExit()
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