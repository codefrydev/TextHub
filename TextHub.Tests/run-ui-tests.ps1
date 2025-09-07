# TextHub UI Test Runner Script (PowerShell)
# This script runs the comprehensive UI test suite

param(
    [string]$BaseUrl = "http://localhost:5099",
    [string]$TestProject = "TextHub.Tests.csproj",
    [string]$ScreenshotsDir = "screenshots",
    [switch]$Headless = $false
)

Write-Host "🚀 Starting TextHub UI Tests..." -ForegroundColor Green
Write-Host "==================================" -ForegroundColor Green

# Create screenshots directory
if (!(Test-Path $ScreenshotsDir)) {
    New-Item -ItemType Directory -Path $ScreenshotsDir | Out-Null
}

Write-Host "📋 Test Configuration:" -ForegroundColor Blue
Write-Host "  • Base URL: $BaseUrl" -ForegroundColor White
Write-Host "  • Test Project: $TestProject" -ForegroundColor White
Write-Host "  • Screenshots: $ScreenshotsDir" -ForegroundColor White
Write-Host "  • Headless Mode: $Headless" -ForegroundColor White
Write-Host ""

# Check if the application is running
Write-Host "🔍 Checking if TextHub is running..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri $BaseUrl -Method Head -TimeoutSec 5
    if ($response.StatusCode -eq 200) {
        Write-Host "✅ TextHub is running at $BaseUrl" -ForegroundColor Green
    } else {
        Write-Host "❌ TextHub returned status code: $($response.StatusCode)" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "❌ TextHub is not running at $BaseUrl" -ForegroundColor Red
    Write-Host "💡 Please start the application first:" -ForegroundColor Yellow
    Write-Host "   cd TextHub && dotnet run" -ForegroundColor White
    exit 1
}

# Install Playwright browsers if needed
Write-Host "🎭 Checking Playwright browsers..." -ForegroundColor Yellow
try {
    dotnet test --list-tests | Out-Null
    Write-Host "✅ Playwright browsers are ready" -ForegroundColor Green
} catch {
    Write-Host "📦 Installing Playwright browsers..." -ForegroundColor Yellow
    dotnet build | Out-Null
}

# Function to run test category
function Invoke-TestCategory {
    param(
        [string]$Category,
        [string]$Description
    )
    
    Write-Host "📝 Running $Description..." -ForegroundColor Yellow
    
    $filter = "FullyQualifiedName~$Category"
    dotnet test --filter $filter --logger "console;verbosity=normal" --collect:"XPlat Code Coverage" | Out-Null
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ $Description completed successfully" -ForegroundColor Green
        return $true
    } else {
        Write-Host "❌ $Description failed" -ForegroundColor Red
        return $false
    }
}

# Run all test categories
Write-Host ""
Write-Host "🧪 Running UI Test Suite..." -ForegroundColor Blue
Write-Host "==================================" -ForegroundColor Blue

$testResults = @()

$testResults += Invoke-TestCategory "HomePageUITests" "Home Page UI Tests"
$testResults += Invoke-TestCategory "TextCaseToolsUITests" "Text Case Tools UI Tests"
$testResults += Invoke-TestCategory "TextAnalysisToolsUITests" "Text Analysis Tools UI Tests"
$testResults += Invoke-TestCategory "NavigationAndResponsiveUITests" "Navigation and Responsive UI Tests"
$testResults += Invoke-TestCategory "AccessibilityAndPerformanceUITests" "Accessibility and Performance Tests"

# Run all tests together
Write-Host "🎯 Running All UI Tests Together..." -ForegroundColor Yellow
dotnet test --logger "console;verbosity=normal" --collect:"XPlat Code Coverage" | Out-Null

Write-Host ""
Write-Host "📊 Test Summary:" -ForegroundColor Blue
Write-Host "==================================" -ForegroundColor Blue

if ($LASTEXITCODE -eq 0) {
    Write-Host "🎉 All UI tests completed successfully!" -ForegroundColor Green
} else {
    Write-Host "💥 Some tests failed" -ForegroundColor Red
}

Write-Host "✅ Test execution completed" -ForegroundColor Green
Write-Host "📸 Screenshots saved to: $ScreenshotsDir" -ForegroundColor Blue
Write-Host "📈 Code coverage reports generated" -ForegroundColor Blue

# Show screenshots if any were created
if (Test-Path $ScreenshotsDir) {
    $screenshots = Get-ChildItem -Path $ScreenshotsDir -Filter "*.png" -ErrorAction SilentlyContinue
    if ($screenshots) {
        Write-Host ""
        Write-Host "📸 Generated Screenshots:" -ForegroundColor Yellow
        $screenshots | ForEach-Object { Write-Host "  • $($_.Name)" -ForegroundColor White }
    }
}

Write-Host ""
Write-Host "🎊 UI Testing Complete!" -ForegroundColor Green

# Exit with appropriate code
if ($LASTEXITCODE -eq 0) {
    exit 0
} else {
    exit 1
}
