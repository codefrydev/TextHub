using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace TextHub.Tests;

[TestFixture]
public class IndividualToolTests : PageTest
{
    private string BaseUrl => GlobalSetup.GetBaseUrl();

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Test]
    public async Task WordCounter_ShouldLoadAndFunction()
    {
        // Navigate to word counter tool
        await Page.GotoAsync($"{BaseUrl}/word-counter");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify page loads
        await Expect(Page).ToHaveTitleAsync("Word Counter - Text Hub");
        
        // Wait for page to fully load
        await Page.WaitForTimeoutAsync(2000);
        
        // Look for text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();
        
        // Test functionality
        await textInput.FillAsync("Hello world! This is a test.");
        await textInput.BlurAsync();
        
        // Wait for any dynamic updates
        await Page.WaitForTimeoutAsync(1000);
        
        // Look for word count display - be more flexible with selectors
        var wordCount = Page.Locator("[class*='count'], [class*='word'], [class*='result'], [class*='output']").First;
        await Expect(wordCount).ToBeVisibleAsync();
    }

    [Test]
    public async Task CharacterCounter_ShouldLoadAndFunction()
    {
        // Navigate to character counter tool
        await Page.GotoAsync($"{BaseUrl}/character-counter");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify page loads
        await Expect(Page).ToHaveTitleAsync("Character Counter - Text Hub");
        
        // Look for text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();
        
        // Test functionality
        await textInput.FillAsync("Test text for counting!");
        await textInput.BlurAsync();
        
        // Look for character count display
        var charCount = Page.Locator("[class*='count'], [class*='character'], [class*='result']");
        await Expect(charCount.First).ToBeVisibleAsync();
    }

    [Test]
    public async Task CamelCase_ShouldLoadAndFunction()
    {
        // Navigate to camel case tool
        await Page.GotoAsync($"{BaseUrl}/camel-case");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify page loads
        await Expect(Page).ToHaveTitleAsync("camelCase Converter - Text Hub");
        
        // Look for text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();
        
        // Test functionality
        await textInput.FillAsync("hello world test");
        await textInput.BlurAsync();
        
        // Look for converted text output
        var output = Page.Locator("[class*='output'], [class*='result'], [class*='converted']");
        await Expect(output.First).ToBeVisibleAsync();
    }

    [Test]
    public async Task PascalCase_ShouldLoadAndFunction()
    {
        // Navigate to pascal case tool
        await Page.GotoAsync($"{BaseUrl}/pascal-case");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify page loads
        await Expect(Page).ToHaveTitleAsync("PascalCase Converter - Text Hub");
        
        // Look for text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();
        
        // Test functionality
        await textInput.FillAsync("hello world test");
        await textInput.BlurAsync();
        
        // Look for converted text output
        var output = Page.Locator("[class*='output'], [class*='result'], [class*='converted']");
        await Expect(output.First).ToBeVisibleAsync();
    }

    [Test]
    public async Task SnakeCase_ShouldLoadAndFunction()
    {
        // Navigate to snake case tool
        await Page.GotoAsync($"{BaseUrl}/snake-case");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify page loads
        await Expect(Page).ToHaveTitleAsync("snake_case Converter - Text Hub");
        
        // Look for text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();
        
        // Test functionality
        await textInput.FillAsync("Hello World Test");
        await textInput.BlurAsync();
        
        // Look for converted text output
        var output = Page.Locator("[class*='output'], [class*='result'], [class*='converted']");
        await Expect(output.First).ToBeVisibleAsync();
    }

    [Test]
    public async Task KebabCase_ShouldLoadAndFunction()
    {
        // Navigate to kebab case tool
        await Page.GotoAsync($"{BaseUrl}/kebab-case");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify page loads
        await Expect(Page).ToHaveTitleAsync("kebab-case Converter - Text Hub");
        
        // Look for text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();
        
        // Test functionality
        await textInput.FillAsync("Hello World Test");
        await textInput.BlurAsync();
        
        // Look for converted text output
        var output = Page.Locator("[class*='output'], [class*='result'], [class*='converted']");
        await Expect(output.First).ToBeVisibleAsync();
    }

    [Test]
    public async Task JsonFormatter_ShouldLoadAndFunction()
    {
        // Navigate to JSON formatter tool
        await Page.GotoAsync($"{BaseUrl}/json-formatter");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify page loads
        await Expect(Page).ToHaveTitleAsync("JSON Formatter - Text Hub");
        
        // Look for text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();
        
        // Test functionality with malformed JSON
        await textInput.FillAsync("{\"name\":\"test\",\"value\":123}");
        await textInput.BlurAsync();
        
        // Look for formatted JSON output
        var output = Page.Locator("[class*='output'], [class*='result'], [class*='formatted']");
        await Expect(output.First).ToBeVisibleAsync();
    }

    [Test]
    public async Task FindReplace_ShouldLoadAndFunction()
    {
        // Navigate to find and replace tool
        await Page.GotoAsync($"{BaseUrl}/find-replace");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify page loads
        await Expect(Page).ToHaveTitleAsync("Find & Replace - Text Hub");
        
        // Look for text input area
        var textInput = Page.Locator("textarea, input[type='text']").First;
        await Expect(textInput).ToBeVisibleAsync();
        
        // Look for find input
        var findInput = Page.Locator("input[placeholder*='find'], input[placeholder*='search']").First;
        await Expect(findInput).ToBeVisibleAsync();
        
        // Look for replace input
        var replaceInput = Page.Locator("input[placeholder*='replace']").First;
        await Expect(replaceInput).ToBeVisibleAsync();
        
        // Test functionality
        await textInput.FillAsync("Hello world! This is a test.");
        await findInput.FillAsync("world");
        await replaceInput.FillAsync("universe");
        
        // Look for result
        var output = Page.Locator("[class*='output'], [class*='result']");
        await Expect(output.First).ToBeVisibleAsync();
    }
}
