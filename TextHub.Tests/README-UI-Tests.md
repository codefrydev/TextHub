# TextHub UI Test Suite

This comprehensive UI test suite provides automated testing for the TextHub application using Playwright and xUnit. The tests are inspired by the manual browser test and cover all major UI functionality.

## 🎯 Test Coverage

### Test Categories

1. **Home Page UI Tests** (`HomePageUITests.cs`)
   - Page loading and title verification
   - Navigation bar functionality
   - Tools dropdown interactions
   - Theme toggle functionality
   - Mobile menu behavior
   - Search component testing

2. **Text Case Tools UI Tests** (`TextCaseToolsUITests.cs`)
   - Lowercase conversion
   - Uppercase conversion
   - Title case conversion
   - Camel case conversion
   - Pascal case conversion
   - Snake case conversion
   - Kebab case conversion
   - Sentence case conversion
   - Input validation and error handling

3. **Text Analysis Tools UI Tests** (`TextAnalysisToolsUITests.cs`)
   - Word counter functionality
   - Character counter
   - Line counter
   - Real-time updates
   - Statistics accuracy
   - Large text handling
   - Special character support

4. **Navigation and Responsive UI Tests** (`NavigationAndResponsiveUITests.cs`)
   - Page navigation
   - Breadcrumb functionality
   - Mobile navigation
   - Responsive design adaptation
   - Deep linking
   - Browser back button
   - Cross-page navigation

5. **Accessibility and Performance Tests** (`AccessibilityAndPerformanceUITests.cs`)
   - ARIA labels and roles
   - Heading structure
   - Form labels
   - Focus management
   - Keyboard navigation
   - Color contrast
   - Performance metrics
   - Resource optimization

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 SDK
- TextHub application running on `http://localhost:5099`
- Playwright browsers installed

### Running Tests

#### Option 1: Using Test Runner Scripts

**Linux/macOS:**
```bash
./run-ui-tests.sh
```

**Windows PowerShell:**
```powershell
.\run-ui-tests.ps1
```

#### Option 2: Using dotnet CLI

**Run all tests:**
```bash
dotnet test
```

**Run specific test category:**
```bash
dotnet test --filter "FullyQualifiedName~HomePageUITests"
```

**Run with detailed output:**
```bash
dotnet test --logger "console;verbosity=detailed"
```

#### Option 3: Using Visual Studio

1. Open the solution in Visual Studio
2. Build the solution
3. Open Test Explorer
4. Run tests individually or by category

## 📸 Screenshots

The test suite automatically captures screenshots during test execution for debugging and verification purposes. Screenshots are saved to the `screenshots/` directory with descriptive names.

## 🔧 Configuration

### Base Test Configuration

The `UITestBase` class provides common configuration:

- **Browser**: Chromium (visible mode for debugging)
- **Viewport**: 1920x1080 (desktop)
- **Timeout**: 30 seconds default
- **SlowMo**: 500ms for better visibility
- **Screenshots**: Automatic capture on failures

### Customizing Tests

You can modify test behavior by:

1. **Changing browser settings** in `UITestBase.cs`:
   ```csharp
   Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
   {
       Headless = true, // Set to true for CI/CD
       SlowMo = 0,      // Remove delay for faster execution
   });
   ```

2. **Adjusting timeouts**:
   ```csharp
   await WaitForElementAsync(selector, timeoutMs: 10000); // 10 second timeout
   ```

3. **Custom viewport sizes**:
   ```csharp
   await Page.SetViewportSizeAsync(375, 667); // Mobile viewport
   ```

## 🎭 Playwright Configuration

The tests use the existing `playwright.config.json` with these key settings:

- **Base URL**: `http://localhost:5099`
- **Headless**: `false` (visible browser for debugging)
- **SlowMo**: `1000ms` (slower execution for visibility)
- **Viewport**: `1920x1080`
- **Browser**: Chromium with Chrome executable

## 📊 Test Structure

### Base Class (`UITestBase.cs`)

Provides common functionality:
- Browser setup and teardown
- Navigation helpers
- Element interaction methods
- Screenshot capture
- Assertion helpers

### Test Classes

Each test class inherits from `UITestBase` and focuses on specific functionality:

```csharp
public class HomePageUITests : UITestBase
{
    [Fact]
    public async Task HomePage_ShouldLoadSuccessfully()
    {
        await NavigateToUrlAsync(BaseUrl);
        await AssertPageTitleAsync("Text Hub - Quick & Simple Text Utilities");
        // ... test implementation
    }
}
```

## 🐛 Debugging Tests

### Running Individual Tests

```bash
dotnet test --filter "FullyQualifiedName~HomePageUITests.HomePage_ShouldLoadSuccessfully"
```

### Debug Mode

Set `Headless = false` in `UITestBase.cs` to see browser interactions:

```csharp
Headless = false, // Browser will be visible
SlowMo = 1000,    // Slower execution for observation
```

### Screenshots

Screenshots are automatically captured:
- On test failures
- At key test points
- For debugging purposes

## 🔍 Test Assertions

The test suite includes comprehensive assertions:

- **Element visibility**: `AssertElementVisibleAsync()`
- **Text content**: `AssertElementTextAsync()`
- **Page titles**: `AssertPageTitleAsync()`
- **Navigation**: `ClickElementAsync()` with navigation waiting
- **Input handling**: `FillInputAsync()`
- **Performance**: Load time and resource usage checks

## 📈 Continuous Integration

For CI/CD pipelines, modify the base configuration:

```csharp
// In UITestBase.cs
Headless = true,  // No visible browser
SlowMo = 0,       // No delays
```

## 🎨 Visual Testing

The tests capture screenshots at key points:
- Page loads
- User interactions
- State changes
- Error conditions

Screenshots help verify:
- UI layout correctness
- Responsive design
- Visual regressions
- Cross-browser compatibility

## 🚨 Troubleshooting

### Common Issues

1. **Application not running**:
   ```
   ❌ TextHub is not running at http://localhost:5099
   ```
   Solution: Start the application with `dotnet run` in the TextHub directory

2. **Playwright browsers not installed**:
   ```
   📦 Installing Playwright browsers...
   ```
   Solution: The script will automatically install browsers

3. **Tests timing out**:
   - Increase timeout values in `UITestBase.cs`
   - Check application performance
   - Verify network connectivity

4. **Element not found**:
   - Check if the application UI has changed
   - Verify selectors are correct
   - Add wait conditions for dynamic content

### Debug Tips

1. **Use visible browser mode** for debugging
2. **Check screenshots** for visual verification
3. **Add delays** for slow-loading content
4. **Verify selectors** match current UI
5. **Check console errors** in browser developer tools

## 📝 Contributing

When adding new tests:

1. Follow the existing naming conventions
2. Use the base class methods for consistency
3. Add appropriate screenshots
4. Include both positive and negative test cases
5. Test responsive behavior
6. Verify accessibility compliance

## 🎯 Best Practices

1. **Test isolation**: Each test should be independent
2. **Clear naming**: Use descriptive test method names
3. **Proper waits**: Wait for elements and network activity
4. **Screenshot capture**: Capture key moments for debugging
5. **Error handling**: Test both success and failure scenarios
6. **Performance**: Monitor test execution time
7. **Maintainability**: Keep tests simple and focused

## 📚 Resources

- [Playwright Documentation](https://playwright.dev/dotnet/)
- [xUnit Documentation](https://xunit.net/)
- [ASP.NET Core Testing](https://docs.microsoft.com/en-us/aspnet/core/test/)
- [Accessibility Testing Guidelines](https://www.w3.org/WAI/test-evaluate/)
