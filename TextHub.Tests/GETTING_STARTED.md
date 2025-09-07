# Getting Started with TextHub UI Tests

This guide will help you set up and run UI tests for the TextHub application using Microsoft Playwright for .NET.

## Quick Start

1. **Start the TextHub application:**
   ```bash
   cd ../TextHub
   dotnet run
   ```

2. **Run the tests:**
   ```bash
   # Windows PowerShell
   .\run-tests.ps1
   
   # Linux/Mac
   ./run-tests.sh
   
   # Or directly with dotnet
   dotnet test
   ```

## Test Structure

### Test Files

- **`PlaywrightTest.cs`** - Basic page load and navigation tests
- **`TextCaseToolsTests.cs`** - Tests for text case conversion tools
- **`TextAnalysisToolsTests.cs`** - Tests for text analysis tools  
- **`TextFormattingToolsTests.cs`** - Tests for text formatting tools
- **`IndividualToolTests.cs`** - Tests for specific tool functionality
- **`AccessibilityAndPerformanceTests.cs`** - Accessibility and performance tests
- **`ExampleToolTest.cs`** - Example tests showing how to test tool functionality

### Configuration Files

- **`playwright.config.json`** - Playwright configuration
- **`TestConfiguration.cs`** - Test configuration and constants
- **`PlaywrightFixture.cs`** - Test setup and teardown
- **`GlobalSetup.cs`** - Global test setup

## Running Tests

### Basic Commands

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "ClassName=PlaywrightTest"

# Run with specific browser
dotnet test --filter "BrowserName=chromium"

# Run in headless mode
HEADLESS=true dotnet test
```

### Advanced Commands

```bash
# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run with HTML report
dotnet test --logger "html;LogFileName=test-report.html"

# Run specific test method
dotnet test --filter "MethodName=HomePage_ShouldLoadSuccessfully"
```

### Using the PowerShell Scripts

```powershell
# Basic test run
.\run-tests.ps1

# Run with specific options
.\run-all-tests.ps1 -Headless -Browser firefox -Timeout 60

# Run specific tests
.\run-all-tests.ps1 -TestFilter "ClassName=TextCaseToolsTests"

# Generate HTML report
.\run-all-tests.ps1 -GenerateReport
```

## Test Configuration

### Environment Variables

- `TEST_ENVIRONMENT` - Set to "local", "staging", or "production"
- `HEADLESS` - Set to "true" to run in headless mode
- `TEST_TIMEOUT` - Set timeout in seconds

### Viewport Sizes

Tests support multiple viewport sizes:
- Mobile: 375x667
- Tablet: 768x1024  
- Desktop: 1920x1080
- Large Desktop: 2560x1440

## Writing New Tests

### Basic Test Structure

```csharp
[TestFixture]
public class MyToolTests : PageTest
{
    private const string BaseUrl = "https://localhost:5001";

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Test]
    public async Task MyTool_ShouldWorkCorrectly()
    {
        // Navigate to tool
        await Page.GotoAsync($"{BaseUrl}/my-tool");
        
        // Find elements
        var input = Page.Locator("textarea").First;
        var output = Page.Locator("[class*='output']").First;
        
        // Test functionality
        await input.FillAsync("test input");
        await input.BlurAsync();
        
        // Verify results
        await Expect(output).ToBeVisibleAsync();
    }
}
```

### Best Practices

1. **Always wait for page load:**
   ```csharp
   await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
   ```

2. **Use specific selectors:**
   ```csharp
   // Good
   var button = Page.Locator("button[type='submit']");
   
   // Avoid
   var button = Page.Locator("button");
   ```

3. **Handle async operations:**
   ```csharp
   await input.FillAsync("text");
   await input.BlurAsync();
   await Page.WaitForTimeoutAsync(500); // Wait for processing
   ```

4. **Use assertions:**
   ```csharp
   await Expect(element).ToBeVisibleAsync();
   await Expect(element).ToHaveTextAsync("expected text");
   ```

## Debugging Tests

### Visual Debugging

1. Set `headless: false` in `playwright.config.json`
2. Add breakpoints in your test code
3. Run tests in debug mode from your IDE

### Screenshots and Videos

- Screenshots are automatically taken on test failures
- Videos are recorded for failed tests
- All artifacts are saved in the `test-results` directory

### Console Output

```csharp
// Enable console logging
Page.Console += (_, msg) => Console.WriteLine($"Console: {msg.Text}");
```

## Troubleshooting

### Common Issues

1. **Application not running:**
   - Ensure TextHub is running on https://localhost:5001
   - Check firewall settings

2. **Browser installation failed:**
   - Run `pwsh bin/Debug/net9.0/playwright.ps1 install`
   - Check internet connection

3. **Tests timing out:**
   - Increase timeout in `playwright.config.json`
   - Check if application is responding slowly

4. **Element not found:**
   - Verify the selector is correct
   - Add wait conditions
   - Check if element is in an iframe

### Getting Help

- Check the [Playwright documentation](https://playwright.dev/dotnet/)
- Review test logs in the `test-results` directory
- Use the `--logger "console;verbosity=detailed"` option for more output

## Continuous Integration

### GitHub Actions Example

```yaml
name: UI Tests
on: [push, pull_request]
jobs:
  test:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    - uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '9.0.x'
    - name: Install Playwright
      run: pwsh bin/Debug/net9.0/playwright.ps1 install
    - name: Run tests
      run: dotnet test --configuration Release
```

This setup provides a comprehensive testing framework for your TextHub application with Playwright!
