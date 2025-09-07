# Comprehensive test runner for TextHub UI tests

param(
    [string]$Environment = "local",
    [switch]$Headless = $false,
    [int]$Timeout = 30,
    [string]$Browser = "chromium",
    [string]$TestFilter = "",
    [switch]$GenerateReport = $false,
    [switch]$Help = $false
)

if ($Help) {
    Write-Host "TextHub UI Test Runner" -ForegroundColor Green
    Write-Host "=====================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Usage: .\run-all-tests.ps1 [options]"
    Write-Host ""
    Write-Host "Options:"
    Write-Host "  -Environment <env>    Test environment (local, staging, production) [default: local]"
    Write-Host "  -Headless            Run tests in headless mode [default: false]"
    Write-Host "  -Timeout <seconds>   Test timeout in seconds [default: 30]"
    Write-Host "  -Browser <browser>   Browser to use (chromium, firefox, webkit) [default: chromium]"
    Write-Host "  -TestFilter <filter> NUnit test filter [default: all tests]"
    Write-Host "  -GenerateReport      Generate HTML test report [default: false]"
    Write-Host "  -Help                Show this help message"
    Write-Host ""
    Write-Host "Examples:"
    Write-Host "  .\run-all-tests.ps1"
    Write-Host "  .\run-all-tests.ps1 -Headless -Browser firefox"
    Write-Host "  .\run-all-tests.ps1 -TestFilter 'ClassName=PlaywrightTest'"
    Write-Host "  .\run-all-tests.ps1 -Environment staging -GenerateReport"
    exit 0
}

Write-Host "TextHub UI Test Runner" -ForegroundColor Green
Write-Host "=====================" -ForegroundColor Green
Write-Host "Environment: $Environment" -ForegroundColor Yellow
Write-Host "Headless: $Headless" -ForegroundColor Yellow
Write-Host "Timeout: $Timeout seconds" -ForegroundColor Yellow
Write-Host "Browser: $Browser" -ForegroundColor Yellow
Write-Host "Test Filter: $TestFilter" -ForegroundColor Yellow
Write-Host ""

# Set environment variables
$env:TEST_ENVIRONMENT = $Environment
$env:HEADLESS = if ($Headless) { "true" } else { "false" }
$env:TEST_TIMEOUT = $Timeout.ToString()

# Build the test project
Write-Host "Building test project..." -ForegroundColor Yellow
dotnet build --configuration Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

# Install Playwright browsers
Write-Host "Installing Playwright browsers..." -ForegroundColor Yellow
pwsh bin/Release/net9.0/playwright.ps1 install

if ($LASTEXITCODE -ne 0) {
    Write-Host "Playwright browser installation failed!" -ForegroundColor Red
    exit 1
}

# Check if application is running
$baseUrl = switch ($Environment) {
    "staging" { "https://codefrydev.in/TextHub" }
    "production" { "https://codefrydev.in/TextHub" }
    default { "https://localhost:5001" }
}

Write-Host "Checking if application is running at $baseUrl..." -ForegroundColor Yellow
try {
    Invoke-WebRequest -Uri $baseUrl -UseBasicParsing -TimeoutSec 10 | Out-Null
    Write-Host "Application is running!" -ForegroundColor Green
} catch {
    Write-Host "Application is not running at $baseUrl" -ForegroundColor Red
    Write-Host "Please start the application and try again." -ForegroundColor Yellow
    exit 1
}

# Prepare test command
$testCommand = "dotnet test --configuration Release --logger `"console;verbosity=normal`""

if ($TestFilter) {
    $testCommand += " --filter `"$TestFilter`""
}

if ($GenerateReport) {
    $testCommand += " --logger `"trx;LogFileName=test-results.trx`" --logger `"html;LogFileName=test-report.html`""
}

# Run tests
Write-Host "Running UI tests..." -ForegroundColor Yellow
Write-Host "Command: $testCommand" -ForegroundColor Cyan
Write-Host ""

Invoke-Expression $testCommand

$testResult = $LASTEXITCODE

# Display results
Write-Host ""
if ($testResult -eq 0) {
    Write-Host "All tests passed!" -ForegroundColor Green
} else {
    Write-Host "Some tests failed. Exit code: $testResult" -ForegroundColor Red
}

# Display test artifacts location
Write-Host ""
Write-Host "Test artifacts:" -ForegroundColor Cyan
Write-Host "- Screenshots: test-results/screenshots/" -ForegroundColor White
Write-Host "- Videos: test-results/videos/" -ForegroundColor White
Write-Host "- Traces: test-results/traces/" -ForegroundColor White

if ($GenerateReport) {
    Write-Host "- HTML Report: test-results/test-report.html" -ForegroundColor White
    Write-Host "- TRX Report: test-results/test-results.trx" -ForegroundColor White
}

Write-Host ""
Write-Host "Test run completed!" -ForegroundColor Green

exit $testResult
