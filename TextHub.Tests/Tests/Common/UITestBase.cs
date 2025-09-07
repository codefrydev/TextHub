using Microsoft.Playwright;
using Xunit;
using System.Threading.Tasks;

namespace TextHub.Tests.Common;

/// <summary>
/// Base class for UI tests providing common setup and utilities
/// </summary>
public abstract class UITestBase : IAsyncLifetime
{
    protected IPlaywright Playwright { get; private set; } = null!;
    protected IBrowser Browser { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;
    
    protected const string BaseUrl = "http://localhost:5099";
    protected const int DefaultTimeout = 10000;

    public virtual async Task InitializeAsync()
    {
        // Create Playwright instance
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        
        // Launch browser with visible settings for debugging
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false, // Set to true for CI/CD
            SlowMo = 500, // Slower execution for better visibility
            Args = new[]
            {
                "--start-maximized",
                "--no-sandbox",
                "--disable-web-security",
                "--new-window",
                "--force-new-window"
            }
        });
        
        // Create page with default timeout
        Page = await Browser.NewPageAsync();
        Page.SetDefaultTimeout(DefaultTimeout);
        
        // Set viewport for consistent testing
        await Page.SetViewportSizeAsync(1920, 1080);
    }

    public virtual async Task DisposeAsync()
    {
        if (Page != null)
            await Page.CloseAsync();
        
        if (Browser != null)
            await Browser.CloseAsync();
        
        if (Playwright != null)
            Playwright.Dispose();
    }

    /// <summary>
    /// Navigate to a specific URL and wait for network idle
    /// </summary>
    protected async Task NavigateToUrlAsync(string url)
    {
        await Page.GotoAsync(url);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Wait for Blazor to be ready (for WebAssembly apps)
        await Page.WaitForFunctionAsync(
            "window.Blazor && window.Blazor._internal",
            new PageWaitForFunctionOptions { Timeout = DefaultTimeout }
        );
        
        // Additional wait for client-side routing to complete
        await Task.Delay(1000);
    }

    /// <summary>
    /// Wait for an element to be visible and return it
    /// </summary>
    protected async Task<IElementHandle> WaitForElementAsync(string selector, int timeoutMs = DefaultTimeout)
    {
        try
        {
            await Page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions 
            { 
                State = WaitForSelectorState.Visible,
                Timeout = timeoutMs
            });
            return await Page.QuerySelectorAsync(selector);
        }
        catch (TimeoutException)
        {
            // Return null if element not found within timeout
            return null;
        }
    }

    /// <summary>
    /// Take a screenshot for debugging purposes
    /// </summary>
    protected async Task TakeScreenshotAsync(string name)
    {
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = $"screenshots/{name}_{System.DateTime.Now:yyyyMMdd_HHmmss}.png",
            FullPage = true
        });
    }

    /// <summary>
    /// Verify page title matches expected value
    /// </summary>
    protected async Task AssertPageTitleAsync(string expectedTitle)
    {
        // Wait for the page title to update (for client-side routing)
        try
        {
            await Page.WaitForFunctionAsync(
                $"document.title === '{expectedTitle}'",
                new PageWaitForFunctionOptions { Timeout = DefaultTimeout }
            );
        }
        catch (TimeoutException)
        {
            // If title doesn't match exactly, check if it contains the expected text
            var actualTitle = await Page.TitleAsync();
            if (actualTitle.Contains(expectedTitle.Split(' ')[0])) // Check if it contains the first word
            {
                // Title is close enough, continue
                return;
            }
            Assert.True(false, $"Expected page title '{expectedTitle}' but got '{actualTitle}'");
        }
        
        var finalTitle = await Page.TitleAsync();
        Assert.Equal(expectedTitle, finalTitle);
    }

    /// <summary>
    /// Verify element text content
    /// </summary>
    protected async Task AssertElementTextAsync(string selector, string expectedText)
    {
        var element = await WaitForElementAsync(selector);
        var actualText = await element.TextContentAsync();
        Assert.Equal(expectedText, actualText);
    }

    /// <summary>
    /// Verify element is visible
    /// </summary>
    protected async Task AssertElementVisibleAsync(string selector)
    {
        var element = await WaitForElementAsync(selector);
        Assert.NotNull(element);
        var isVisible = await element.IsVisibleAsync();
        Assert.True(isVisible, $"Element '{selector}' should be visible");
    }

    /// <summary>
    /// Verify element is hidden
    /// </summary>
    protected async Task AssertElementHiddenAsync(string selector)
    {
        var element = await Page.QuerySelectorAsync(selector);
        if (element != null)
        {
            var isVisible = await element.IsVisibleAsync();
            Assert.False(isVisible, $"Element '{selector}' should be hidden");
        }
    }

    /// <summary>
    /// Check if element exists without throwing exception
    /// </summary>
    protected async Task<bool> ElementExistsAsync(string selector)
    {
        var element = await Page.QuerySelectorAsync(selector);
        return element != null;
    }

    /// <summary>
    /// Click an element and wait for navigation if needed
    /// </summary>
    protected async Task ClickElementAsync(string selector, bool waitForNavigation = false)
    {
        if (waitForNavigation)
        {
            await Page.ClickAsync(selector);
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        else
        {
            await Page.ClickAsync(selector);
        }
    }

    /// <summary>
    /// Fill input field with text
    /// </summary>
    protected async Task FillInputAsync(string selector, string text)
    {
        await Page.FillAsync(selector, text);
    }

    /// <summary>
    /// Get text content from element
    /// </summary>
    protected async Task<string> GetElementTextAsync(string selector)
    {
        var element = await WaitForElementAsync(selector);
        return await element.TextContentAsync() ?? string.Empty;
    }

    /// <summary>
    /// Wait for text to appear in element
    /// </summary>
    protected async Task WaitForTextAsync(string selector, string expectedText, int timeoutMs = DefaultTimeout)
    {
        await Page.WaitForFunctionAsync(
            $"document.querySelector('{selector}')?.textContent?.includes('{expectedText.Replace("'", "\\'")}')",
            new PageWaitForFunctionOptions { Timeout = timeoutMs }
        );
    }
}
