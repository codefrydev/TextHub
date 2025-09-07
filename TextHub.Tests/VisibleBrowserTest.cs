using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace TextHub.Tests;

/// <summary>
/// Simple test to demonstrate visible browser functionality
/// This test will run for 10 seconds so you can see the browser in action
/// </summary>
[TestFixture]
public class VisibleBrowserTest : PageTest
{
    private string BaseUrl => GlobalSetup.GetBaseUrl();

    [Test]
    [Timeout(30000)] // 30 second timeout
    public async Task Browser_ShouldBeVisible()
    {
        // Navigate to the home page
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify the page loaded
        var title = await Page.TitleAsync();
        Assert.That(title, Is.Not.Null.And.Not.Empty, "Page should have a title");
        
        // Wait for 10 seconds so you can see the browser
        Console.WriteLine("Browser should be visible now! You can see the TextHub application running.");
        Console.WriteLine("The test will wait for 10 seconds so you can interact with the page if needed.");
        
        await Page.WaitForTimeoutAsync(10000); // Wait 10 seconds
        
        // Take a screenshot to prove the test ran
        var screenshot = await Page.ScreenshotAsync();
        Assert.That(screenshot.Length, Is.GreaterThan(0), "Screenshot should be taken");
        
        Console.WriteLine("Test completed! Browser will close now.");
    }

    [Test]
    [Timeout(30000)]
    public async Task Browser_ShouldNavigateToTools()
    {
        // Navigate to the home page
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Wait a bit to see the home page
        await Page.WaitForTimeoutAsync(2000);
        
        // Navigate to word counter tool
        await Page.GotoAsync($"{BaseUrl}/word-counter");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Wait to see the tool page
        await Page.WaitForTimeoutAsync(3000);
        
        // Navigate to camel case tool
        await Page.GotoAsync($"{BaseUrl}/camel-case");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Wait to see the tool page
        await Page.WaitForTimeoutAsync(3000);
        
        Console.WriteLine("Successfully navigated through different pages!");
    }
}
