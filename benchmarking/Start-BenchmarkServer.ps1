param (
    $Bind = "0.0.0.0",
    $Port = 8080,
    $Framework = "net5.0",
    $BuildConfiguration = "Release",
    $ProjectFile = "$(Split-Path $MyInvocation.MyCommand.Definition -Parent)\..\Mastersign.MicroHttpServer.Benchmark\Mastersign.MicroHttpServer.Benchmark.csproj",
    $Job = "Minimal",
    $LogLevel = "Information",
    [switch]$LogWithColors,
    [switch]$NoDelay
)
Write-Host $ProjectFile

$serverArgs = @(
    "run"
    "-f", $Framework
    "-c", $BuildConfiguration
    "-p", (Resolve-Path $ProjectFile)
    "--no-launch-profile"
    "-Host", $Bind
    "-Port", $Port
    "-LogToConsole", "-LogLevel", $LogLevel
)
if ($LogWithColors) { $serverArgs += "-LogWithColors" }
if ($NoDelay) { $serverArgs += "-NoDelay" }
$serverArgs += $Job

# Write-Host "CommandLine: dotnet $([string]::Join(" ", $serverArgs))"
Start-Process "dotnet" -ArgumentList $serverArgs -Wait -NoNewWindow
