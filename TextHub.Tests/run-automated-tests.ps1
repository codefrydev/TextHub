# PowerShell script to run tests with automated server startup and visible browser

Write-Host "TextHub Automated UI Tests with Playwright" -ForegroundColor Green
Write-Host "===========================================" -ForegroundColor Green

# Set environment variables for visible testing
$env:HEADLESS = "false"
$env:TEST_ENVIRONMENT = "local"

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

# Run tests (the GlobalSetup will automatically start the TextHub server)
Write-Host "`nRunning automated UI tests..." -ForegroundColor Yellow
Write-Host "The TextHub server will be started automatically!" -ForegroundColor Cyan
Write-Host "You should see browser windows open during the tests!" -ForegroundColor Cyan

if ($args.Count -gt 0) {
    # Run specific test if filter provided
    $filter = $args[0]
    Write-Host "Running tests matching: $filter" -ForegroundColor Cyan
    dotnet test --filter $filter --logger "console;verbosity=normal"
} else {
    # Run all tests
    Write-Host "Running all tests..." -ForegroundColor Cyan
    dotnet test --logger "console;verbosity=normal"
}

if ($LASTEXITCODE -eq 0) {
    Write-Host "`nAll tests passed!" -ForegroundColor Green
} else {
    Write-Host "`nSome tests failed. Check the output above for details." -ForegroundColor Red
}

Write-Host "`nTest run completed!" -ForegroundColor Green
Write-Host "Test results and screenshots are available in the 'test-results' directory." -ForegroundColor Cyan
