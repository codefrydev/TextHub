using Microsoft.Playwright;
using Xunit;
using System.Threading.Tasks;
using TextHub.Tests.Common;

namespace TextHub.Tests.Features;

/// <summary>
/// UI tests for text case conversion tools
/// </summary>
public class TextCaseToolsUITests : UITestBase
{
    [Fact]
    public async Task LowercaseTool_ShouldConvertTextCorrectly()
    {
        await NavigateToUrlAsync($"{BaseUrl}/lowercase");
        
        // Verify page loads correctly - check for tool-specific content
        await AssertElementVisibleAsync("h1:has-text('Lowercase Converter')");
        
        // Wait a bit for any client-side routing to complete
        await Task.Delay(2000);
        
        // Find and fill input textarea
        var inputSelector = "textarea, input[type='text']";
        await WaitForElementAsync(inputSelector);
        
        // Test conversion
        await FillInputAsync(inputSelector, "HELLO WORLD");
        await Task.Delay(500); // Wait for conversion
        
        // Look for output or converted text
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal("hello world", outputText);
        }
        
        await TakeScreenshotAsync("lowercase_conversion");
    }

    [Fact]
    public async Task UppercaseTool_ShouldConvertTextCorrectly()
    {
        await NavigateToUrlAsync($"{BaseUrl}/uppercase");
        
        // Verify page loads correctly - check for tool-specific content
        await AssertElementVisibleAsync("h1:has-text('Uppercase Converter')");
        
        // Wait a bit for any client-side routing to complete
        await Task.Delay(2000);
        
        // Find and fill input textarea
        var inputSelector = "textarea, input[type='text']";
        await WaitForElementAsync(inputSelector);
        
        // Test conversion
        await FillInputAsync(inputSelector, "hello world");
        await Task.Delay(500); // Wait for conversion
        
        // Look for output or converted text
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal("HELLO WORLD", outputText);
        }
        
        await TakeScreenshotAsync("uppercase_conversion");
    }

    [Fact]
    public async Task TitleCaseTool_ShouldConvertTextCorrectly()
    {
        await NavigateToUrlAsync($"{BaseUrl}/title-case");
        
        // Verify page loads correctly - check for tool-specific content
        await AssertElementVisibleAsync("h1:has-text('Title Case Converter')");
        
        // Wait a bit for any client-side routing to complete
        await Task.Delay(2000);
        
        // Find and fill input textarea
        var inputSelector = "textarea, input[type='text']";
        await WaitForElementAsync(inputSelector);
        
        // Test conversion
        await FillInputAsync(inputSelector, "hello world test");
        await Task.Delay(500); // Wait for conversion
        
        // Look for output or converted text
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal("Hello World Test", outputText);
        }
        
        await TakeScreenshotAsync("titlecase_conversion");
    }

    [Fact]
    public async Task CamelCaseTool_ShouldConvertTextCorrectly()
    {
        await NavigateToUrlAsync($"{BaseUrl}/camel-case");
        
        // Verify page loads correctly - check for tool-specific content
        await AssertElementVisibleAsync("h1:has-text('camelCase Converter')");
        
        // Wait a bit for any client-side routing to complete
        await Task.Delay(2000);
        
        // Find and fill input textarea
        var inputSelector = "textarea, input[type='text']";
        await WaitForElementAsync(inputSelector);
        
        // Test conversion
        await FillInputAsync(inputSelector, "hello world test");
        await Task.Delay(500); // Wait for conversion
        
        // Look for output or converted text
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal("helloWorldTest", outputText);
        }
        
        await TakeScreenshotAsync("camelcase_conversion");
    }

    [Fact]
    public async Task PascalCaseTool_ShouldConvertTextCorrectly()
    {
        await NavigateToUrlAsync($"{BaseUrl}/pascal-case");
        
        // Verify page loads correctly - check for tool-specific content
        await AssertElementVisibleAsync("h1:has-text('PascalCase Converter')");
        
        // Wait a bit for any client-side routing to complete
        await Task.Delay(2000);
        
        // Find and fill input textarea
        var inputSelector = "textarea, input[type='text']";
        await WaitForElementAsync(inputSelector);
        
        // Test conversion
        await FillInputAsync(inputSelector, "hello world test");
        await Task.Delay(500); // Wait for conversion
        
        // Look for output or converted text
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal("HelloWorldTest", outputText);
        }
        
        await TakeScreenshotAsync("pascalcase_conversion");
    }

    [Fact]
    public async Task SnakeCaseTool_ShouldConvertTextCorrectly()
    {
        await NavigateToUrlAsync($"{BaseUrl}/snake-case");
        
        // Verify page loads correctly - check for tool-specific content
        await AssertElementVisibleAsync("h1:has-text('snake_case Converter')");
        
        // Wait a bit for any client-side routing to complete
        await Task.Delay(2000);
        
        // Find and fill input textarea
        var inputSelector = "textarea, input[type='text']";
        await WaitForElementAsync(inputSelector);
        
        // Test conversion
        await FillInputAsync(inputSelector, "Hello World Test");
        await Task.Delay(500); // Wait for conversion
        
        // Look for output or converted text
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal("hello_world_test", outputText);
        }
        
        await TakeScreenshotAsync("snakecase_conversion");
    }

    [Fact]
    public async Task KebabCaseTool_ShouldConvertTextCorrectly()
    {
        await NavigateToUrlAsync($"{BaseUrl}/kebab-case");
        
        // Verify page loads correctly - check for tool-specific content
        await AssertElementVisibleAsync("h1:has-text('kebab-case Converter')");
        
        // Wait a bit for any client-side routing to complete
        await Task.Delay(2000);
        
        // Find and fill input textarea
        var inputSelector = "textarea, input[type='text']";
        await WaitForElementAsync(inputSelector);
        
        // Test conversion
        await FillInputAsync(inputSelector, "Hello World Test");
        await Task.Delay(500); // Wait for conversion
        
        // Look for output or converted text
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal("hello-world-test", outputText);
        }
        
        await TakeScreenshotAsync("kebabcase_conversion");
    }

    [Fact]
    public async Task SentenceCaseTool_ShouldConvertTextCorrectly()
    {
        await NavigateToUrlAsync($"{BaseUrl}/sentence-case");
        
        // Verify page loads correctly - check for tool-specific content
        await AssertElementVisibleAsync("h1:has-text('Sentence Case Converter')");
        
        // Wait a bit for any client-side routing to complete
        await Task.Delay(2000);
        
        // Find and fill input textarea
        var inputSelector = "textarea, input[type='text']";
        await WaitForElementAsync(inputSelector);
        
        // Test conversion
        await FillInputAsync(inputSelector, "HELLO WORLD. THIS IS A TEST.");
        await Task.Delay(500); // Wait for conversion
        
        // Look for output or converted text
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal("Hello world. This is a test.", outputText);
        }
        
        await TakeScreenshotAsync("sentencecase_conversion");
    }

    [Fact]
    public async Task TextCaseToolLayout_ShouldHaveCorrectElements()
    {
        await NavigateToUrlAsync($"{BaseUrl}/lowercase");
        
        // Verify page structure
        await AssertElementVisibleAsync("h1");
        await AssertElementVisibleAsync("main");
        
        // Verify input area exists
        await AssertElementVisibleAsync("textarea, input[type='text']");
        
        // Verify convert button exists
        var convertButton = await Page.QuerySelectorAsync("button:has-text('Convert'), button:has-text('lowercase')");
        Assert.NotNull(convertButton);
        
        // Verify description text
        await AssertElementVisibleAsync("p, div:has-text('Transform text')");
        
        await TakeScreenshotAsync("text_case_tool_layout");
    }

    [Fact]
    public async Task TextCaseTool_ShouldHandleEmptyInput()
    {
        await NavigateToUrlAsync($"{BaseUrl}/lowercase");
        
        // Clear input and test empty conversion
        var inputSelector = "textarea, input[type='text']";
        await FillInputAsync(inputSelector, "");
        await Task.Delay(500);
        
        // Verify no errors occur
        var errorElement = await Page.QuerySelectorAsync(".error, .alert");
        Assert.Null(errorElement);
        
        await TakeScreenshotAsync("empty_input_test");
    }

    [Fact]
    public async Task TextCaseTool_ShouldHandleSpecialCharacters()
    {
        await NavigateToUrlAsync($"{BaseUrl}/lowercase");
        
        // Test with special characters
        var inputSelector = "textarea, input[type='text']";
        var testText = "HELLO! @#$%^&*() WORLD!";
        await FillInputAsync(inputSelector, testText);
        await Task.Delay(500);
        
        // Verify conversion handles special characters
        var outputElement = await Page.QuerySelectorAsync("textarea:not([placeholder*='Enter']), .output, .result");
        if (outputElement != null)
        {
            var outputText = await outputElement.InputValueAsync();
            Assert.Equal(testText.ToLower(), outputText);
        }
        
        await TakeScreenshotAsync("special_characters_test");
    }
}
