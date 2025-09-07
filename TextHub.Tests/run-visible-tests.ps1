# PowerShell script to run tests with visible browser

Write-Host "Running Playwright tests with visible browser..." -ForegroundColor Green
Write-Host "=============================================" -ForegroundColor Green

# Set environment variable to make browser visible
$env:HEADLESS = "false"

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
Write-Host "`nRunning tests with visible browser..." -ForegroundColor Yellow
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
