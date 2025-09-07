using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace TextHub.Tests;

/// <summary>
/// Simple test to verify Playwright setup is working correctly
/// </summary>
[TestFixture]
public class PlaywrightSetupTest : PageTest
{
    [Test]
    [Timeout(60000)] // 60 second timeout
    public async Task Playwright_ShouldBeWorking()
    {
        // Test that we can navigate to a simple page
        await Page.GotoAsync("https://example.com", new PageGotoOptions { Timeout = 60000 });
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify the page loaded
        var title = await Page.TitleAsync();
        Assert.That(title, Is.Not.Null.And.Not.Empty, "Page title should not be empty");
        
        // Verify we can find elements
        var heading = Page.Locator("h1");
        await Expect(heading).ToBeVisibleAsync();
    }

    [Test]
    public async Task Playwright_ShouldHandleJavaScript()
    {
        // Test JavaScript execution
        await Page.GotoAsync("https://example.com");
        
        // Execute JavaScript
        var result = await Page.EvaluateAsync<string>("() => document.title");
        Assert.That(result, Is.Not.Null.And.Not.Empty, "JavaScript execution should work");
    }

    [Test]
    public async Task Playwright_ShouldTakeScreenshots()
    {
        // Test screenshot capability
        await Page.GotoAsync("https://example.com");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Take a screenshot
        var screenshot = await Page.ScreenshotAsync();
        Assert.That(screenshot.Length, Is.GreaterThan(0), "Screenshot should be taken successfully");
    }
}
