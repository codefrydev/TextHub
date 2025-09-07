using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace TextHub.Tests;

/// <summary>
/// Example test demonstrating how to test a specific tool functionality
/// This serves as a template for creating more specific tool tests
/// </summary>
[TestFixture]
public class ExampleToolTest : PageTest
{
    private string BaseUrl => GlobalSetup.GetBaseUrl();

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Test]
    public async Task Example_WordCounterTool_ShouldWorkCorrectly()
    {
        // Navigate to the word counter tool
        await Page.GotoAsync($"{BaseUrl}/word-counter");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Verify the page title
        await Expect(Page).ToHaveTitleAsync("Word Counter - Text Hub");

        // Find the text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();

        // Test with sample text
        var testText = "Hello world! This is a test with 8 words.";
        await textInput.FillAsync(testText);
        
        // Trigger any change events
        await textInput.BlurAsync();
        await Page.WaitForTimeoutAsync(500); // Wait for any async processing

        // Look for word count display
        var wordCountElement = Page.Locator("[class*='count'], [class*='word'], [class*='result']").First;
        await Expect(wordCountElement).ToBeVisibleAsync();

        // Get the displayed count
        var displayedCount = await wordCountElement.TextContentAsync();
        
        // Verify the count is reasonable (exact count depends on implementation)
        Assert.That(displayedCount, Is.Not.Null.And.Not.Empty, "Word count should be displayed");
        
        // You could add more specific assertions based on your implementation
        // For example, if the count is displayed as "8 words":
        // Assert.That(displayedCount, Does.Contain("8"));
    }

    [Test]
    public async Task Example_TextCaseConversion_ShouldWorkCorrectly()
    {
        // Navigate to a text case conversion tool (e.g., camel case)
        await Page.GotoAsync($"{BaseUrl}/camel-case");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Verify the page title
        await Expect(Page).ToHaveTitleAsync("camelCase Converter - Text Hub");

        // Find the input and output areas
        var textInput = Page.Locator("textarea, input[type='text']").First;
        var textOutput = Page.Locator("[class*='output'], [class*='result'], [class*='converted']").First;

        await Expect(textInput).ToBeVisibleAsync();
        await Expect(textOutput).ToBeVisibleAsync();

        // Test with sample text
        var inputText = "hello world test";

        await textInput.FillAsync(inputText);
        await textInput.BlurAsync();
        await Page.WaitForTimeoutAsync(500); // Wait for conversion

        // Get the converted text
        var actualOutput = await textOutput.TextContentAsync();
        
        // Verify the conversion worked
        Assert.That(actualOutput, Is.Not.Null.And.Not.Empty, "Converted text should be displayed");
        
        // Note: The exact assertion depends on your implementation
        // You might need to adjust this based on how the output is formatted
        // Assert.That(actualOutput.Trim(), Is.EqualTo(expectedOutput));
    }

    [Test]
    public async Task Example_JsonFormatter_ShouldFormatJson()
    {
        // Navigate to the JSON formatter tool
        await Page.GotoAsync($"{BaseUrl}/json-formatter");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Verify the page title
        await Expect(Page).ToHaveTitleAsync("JSON Formatter - Text Hub");

        // Find the input and output areas
        var jsonInput = Page.Locator("textarea, input[type='text']").First;
        var jsonOutput = Page.Locator("[class*='output'], [class*='result'], [class*='formatted']").First;

        await Expect(jsonInput).ToBeVisibleAsync();
        await Expect(jsonOutput).ToBeVisibleAsync();

        // Test with minified JSON
        var minifiedJson = "{\"name\":\"test\",\"value\":123,\"items\":[1,2,3]}";
        await jsonInput.FillAsync(minifiedJson);
        await jsonInput.BlurAsync();
        await Page.WaitForTimeoutAsync(500); // Wait for formatting

        // Get the formatted JSON
        var formattedJson = await jsonOutput.TextContentAsync();
        
        // Verify the formatting worked
        Assert.That(formattedJson, Is.Not.Null.And.Not.Empty, "Formatted JSON should be displayed");
        
        // The formatted JSON should be different from the input (more readable)
        Assert.That(formattedJson, Is.Not.EqualTo(minifiedJson), "Formatted JSON should be different from input");
    }

    [Test]
    public async Task Example_Tool_ShouldHandleEmptyInput()
    {
        // Navigate to any tool (using word counter as example)
        await Page.GotoAsync($"{BaseUrl}/word-counter");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Find the text input
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();

        // Test with empty input
        await textInput.FillAsync("");
        await textInput.BlurAsync();
        await Page.WaitForTimeoutAsync(500);

        // Verify the tool handles empty input gracefully
        // (No errors should occur, and output should show appropriate message)
        var output = Page.Locator("[class*='count'], [class*='result'], [class*='output']").First;
        await Expect(output).ToBeVisibleAsync();

        // The output should indicate zero or empty result
        var outputText = await output.TextContentAsync();
        Assert.That(outputText, Is.Not.Null, "Output should be displayed even for empty input");
    }

    [Test]
    public async Task Example_Tool_ShouldHandleLargeInput()
    {
        // Navigate to any tool (using word counter as example)
        await Page.GotoAsync($"{BaseUrl}/word-counter");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Find the text input
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();

        // Test with large input
        var largeText = string.Join(" ", Enumerable.Repeat("word", 1000)); // 1000 words
        await textInput.FillAsync(largeText);
        await textInput.BlurAsync();
        await Page.WaitForTimeoutAsync(1000); // Wait longer for processing

        // Verify the tool handles large input without issues
        var output = Page.Locator("[class*='count'], [class*='result'], [class*='output']").First;
        await Expect(output).ToBeVisibleAsync();

        // The output should show the correct count
        var outputText = await output.TextContentAsync();
        Assert.That(outputText, Is.Not.Null.And.Not.Empty, "Output should be displayed for large input");
    }
}
