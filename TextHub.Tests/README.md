# TextHub UI Tests with Playwright

This project contains UI tests for the TextHub application using Microsoft Playwright for .NET.

## Prerequisites

- .NET 9.0 SDK
- Visual Studio or VS Code
- The TextHub application running locally

## Setup

1. **Install Playwright browsers:**
   ```bash
   dotnet build
   pwsh bin/Debug/net9.0/playwright.ps1 install
   ```

2. **Start the TextHub application:**
   ```bash
   cd ../TextHub
   dotnet run
   ```

3. **Run the tests:**
   ```bash
   dotnet test
   ```

## Test Structure

- `PlaywrightTest.cs` - Basic page load and navigation tests
- `TextCaseToolsTests.cs` - Tests for text case conversion tools
- `TextAnalysisToolsTests.cs` - Tests for text analysis tools
- `TextFormattingToolsTests.cs` - Tests for text formatting tools
- `PlaywrightFixture.cs` - Test configuration and setup
- `GlobalSetup.cs` - Global test setup for browser installation

## Configuration

The tests are configured in `playwright.config.json` with:
- Base URL: `https://localhost:5001`
- Headless mode: `false` (for debugging)
- Viewport: 1920x1080
- Retries: 2
- Screenshots and videos on failure

## Running Specific Tests

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "ClassName=PlaywrightTest"

# Run with specific browser
dotnet test --filter "BrowserName=chromium"

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"
```

## Debugging

1. Set `headless: false` in `playwright.config.json`
2. Add breakpoints in your test code
3. Run tests in debug mode from your IDE

## Test Reports

Test results and screenshots are saved in the `test-results` directory after test execution.
