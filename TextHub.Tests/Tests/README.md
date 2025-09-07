# Test Organization

This directory contains all UI tests organized by functionality and purpose for better maintainability.

## Folder Structure

### 📁 Common
Contains shared test utilities and base classes:
- `UITestBase.cs` - Base class for all UI tests with common setup and helper methods
- `ManualBrowserTest.cs` - Manual browser testing utilities

### 📁 Pages
Contains tests for specific pages and page-level functionality:
- `HomePageUITests.cs` - Tests for the home page functionality
- `NavigationAndResponsiveUITests.cs` - Tests for navigation and responsive design

### 📁 Features
Contains tests for specific application features:
- `TextAnalysisToolsUITests.cs` - Tests for text analysis tools (Word Counter, Character Counter, etc.)
- `TextCaseToolsUITests.cs` - Tests for text case conversion tools (Uppercase, Lowercase, etc.)

### 📁 Components
Reserved for component-specific tests (currently empty, ready for future component tests)

### 📁 Accessibility
Contains accessibility and usability tests:
- `AccessibilityAndPerformanceUITests.cs` - Tests for accessibility compliance and performance metrics

### 📁 Performance
Reserved for dedicated performance tests (currently empty, ready for future performance-specific tests)

### 📁 Integration
Contains end-to-end integration tests:
- `BlazorProjectUITests.cs` - Comprehensive tests based on actual Blazor project structure

## Running Tests

### Run All Tests
```bash
dotnet test
```

### Run Tests by Category
```bash
# Run only page tests
dotnet test --filter "FullyQualifiedName~Pages"

# Run only feature tests
dotnet test --filter "FullyQualifiedName~Features"

# Run only accessibility tests
dotnet test --filter "FullyQualifiedName~Accessibility"

# Run only integration tests
dotnet test --filter "FullyQualifiedName~Integration"
```

### Run Specific Test Classes
```bash
# Run home page tests
dotnet test --filter "HomePageUITests"

# Run text analysis tests
dotnet test --filter "TextAnalysisToolsUITests"
```

## Test Naming Conventions

- **Test Classes**: `[Feature]UITests.cs` (e.g., `HomePageUITests.cs`)
- **Test Methods**: `[Feature]_Should[ExpectedBehavior]` (e.g., `HomePage_ShouldLoadSuccessfully`)
- **Namespaces**: `TextHub.Tests.[Category]` (e.g., `TextHub.Tests.Pages`)

## Adding New Tests

1. **Choose the appropriate folder** based on what you're testing
2. **Follow naming conventions** for classes and methods
3. **Inherit from UITestBase** for UI tests
4. **Add proper using statements** to reference the Common namespace
5. **Update this README** if adding new categories

## Best Practices

- Keep tests focused and atomic
- Use descriptive test names
- Include proper setup and teardown
- Add screenshots for debugging
- Handle timeouts gracefully
- Make tests independent and repeatable
