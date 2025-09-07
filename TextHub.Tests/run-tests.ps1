# PowerShell script to run TextHub UI tests with Playwright

Write-Host "TextHub UI Tests with Playwright" -ForegroundColor Green
Write-Host "=================================" -ForegroundColor Green

# Check if .NET is installed
try {
    $dotnetVersion = dotnet --version
    Write-Host "Using .NET version: $dotnetVersion" -ForegroundColor Yellow
} catch {
    Write-Host "Error: .NET is not installed or not in PATH" -ForegroundColor Red
    exit 1
}

# Build the test project
Write-Host "`nBuilding test project..." -ForegroundColor Yellow
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

# Install Playwright browsers
Write-Host "`nInstalling Playwright browsers..." -ForegroundColor Yellow
pwsh bin/Debug/net9.0/playwright.ps1 install

if ($LASTEXITCODE -ne 0) {
    Write-Host "Playwright browser installation failed!" -ForegroundColor Red
    exit 1
}

# Check if TextHub is running
Write-Host "`nChecking if TextHub is running..." -ForegroundColor Yellow
try {
    Invoke-WebRequest -Uri "https://localhost:5001" -UseBasicParsing -TimeoutSec 5 | Out-Null
    Write-Host "TextHub is running!" -ForegroundColor Green
} catch {
    Write-Host "TextHub is not running. Please start it with 'dotnet run' in the TextHub directory." -ForegroundColor Red
    Write-Host "Then run this script again." -ForegroundColor Yellow
    exit 1
}

# Run tests
Write-Host "`nRunning UI tests..." -ForegroundColor Yellow
dotnet test --logger "console;verbosity=normal"

if ($LASTEXITCODE -eq 0) {
    Write-Host "`nAll tests passed!" -ForegroundColor Green
} else {
    Write-Host "`nSome tests failed. Check the output above for details." -ForegroundColor Red
}

Write-Host "`nTest results and screenshots are available in the 'test-results' directory." -ForegroundColor Cyan
