using Microsoft.Playwright;
using Xunit;
using System.Threading.Tasks;

namespace TextHub.Tests.Common;

public class ManualBrowserTest
{
    [Fact]
    public async Task LaunchVisibleBrowser()
    {
        // Create Playwright instance
        using var playwright = await Playwright.CreateAsync();
        
        // Launch browser with explicit visible settings
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 1000,
            Args = new[]
            {
                "--start-maximized",
                "--no-sandbox",
                "--disable-web-security",
                "--new-window",
                "--force-new-window"
            }
        });
        
        // Create page
        var page = await browser.NewPageAsync();
        
        // Navigate to TextHub
        await page.GotoAsync("http://localhost:5099");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Show alert
        await page.EvaluateAsync("alert('MANUAL BROWSER LAUNCH - Can you see this?');");
        await Task.Delay(3000);
        
        // Verify page title
        var title = await page.TitleAsync();
        Assert.Equal("Text Hub - Quick & Simple Text Utilities", title);
        
        // Close browser
        await browser.CloseAsync();
    }
}
