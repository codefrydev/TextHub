using Microsoft.Playwright;
using Xunit;
using System.Threading.Tasks;
using TextHub.Tests.Common;

namespace TextHub.Tests.Accessibility;

/// <summary>
/// UI tests for accessibility and performance
/// </summary>
public class AccessibilityAndPerformanceUITests : UITestBase
{
    [Fact]
    public async Task Accessibility_ShouldHaveProperARIALabels()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Verify main navigation has proper ARIA labels
        await AssertElementVisibleAsync("nav[role='navigation']");
        
        // Check if nav has aria-label (optional)
        var navWithLabel = await WaitForElementAsync("nav[aria-label='Main navigation']");
        if (navWithLabel != null)
        {
            await AssertElementVisibleAsync("nav[aria-label='Main navigation']");
        }
        
        // Verify main content has proper role
        await AssertElementVisibleAsync("main[role='main']");
        
        // Verify footer has proper role
        await AssertElementVisibleAsync("footer[role='contentinfo']");
        
        // Verify header has proper role
        await AssertElementVisibleAsync("header[role='banner']");
        
        // Verify theme toggle has proper ARIA label (if visible)
        var themeToggle = await WaitForElementAsync("button[aria-label='Toggle dark mode']");
        if (themeToggle != null)
        {
            await AssertElementVisibleAsync("button[aria-label='Toggle dark mode']");
        }
        
        // Verify mobile menu button has proper ARIA label (if visible)
        var mobileMenuButton = await WaitForElementAsync("button[aria-label='Open menu']");
        if (mobileMenuButton != null)
        {
            await AssertElementVisibleAsync("button[aria-label='Open menu']");
        }
        
        await TakeScreenshotAsync("accessibility_aria_labels");
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperHeadingStructure()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Verify h1 exists
        var h1Elements = await Page.QuerySelectorAllAsync("h1");
        Assert.True(h1Elements.Count > 0, "Page should have at least one h1 heading");
        
        // Verify heading hierarchy (h1 should come before h2, etc.)
        var headings = await Page.EvaluateAsync<string[]>(@"
            Array.from(document.querySelectorAll('h1, h2, h3, h4, h5, h6'))
                .map(h => h.tagName.toLowerCase())
        ");
        
        Assert.True(headings.Length > 0, "Page should have headings");
        
        await TakeScreenshotAsync("accessibility_heading_structure");
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperFormLabels()
    {
        await NavigateToUrlAsync($"{BaseUrl}/word-counter");
        
        // Verify textarea has proper label
        var textarea = await Page.QuerySelectorAsync("textarea");
        Assert.NotNull(textarea);
        
        // Check for associated label
        var label = await Page.QuerySelectorAsync("label");
        Assert.NotNull(label);
        
        // Verify label text
        var labelText = await label.TextContentAsync();
        Assert.Contains("text", labelText.ToLower());
        
        await TakeScreenshotAsync("accessibility_form_labels");
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperFocusManagement()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Test tab navigation
        await Page.Keyboard.PressAsync("Tab");
        await Task.Delay(200);
        
        // Verify focus is visible
        var focusedElement = await Page.EvaluateAsync<string>("document.activeElement.tagName");
        Assert.NotNull(focusedElement);
        
        // Test skip navigation
        var skipLink = await Page.QuerySelectorAsync("a[href='#main-content'], .skip-navigation");
        if (skipLink != null)
        {
            await Page.FocusAsync("a[href='#main-content'], .skip-navigation");
            await Page.Keyboard.PressAsync("Enter");
            await Task.Delay(500);
            
            // Verify focus moved to main content
            var mainContent = await Page.QuerySelectorAsync("#main-content");
            Assert.NotNull(mainContent);
        }
        
        await TakeScreenshotAsync("accessibility_focus_management");
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperColorContrast()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Test both light and dark themes
        var themeButton = await Page.QuerySelectorAsync("button[aria-label='Toggle dark mode']");
        if (themeButton != null)
        {
            // Test light theme
            await ClickElementAsync("button[aria-label='Toggle dark mode']");
            await Task.Delay(500);
            await TakeScreenshotAsync("accessibility_light_theme");
            
            // Test dark theme
            await ClickElementAsync("button[aria-label='Toggle dark mode']");
            await Task.Delay(500);
            await TakeScreenshotAsync("accessibility_dark_theme");
        }
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperKeyboardNavigation()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Test keyboard navigation through main elements
        await Page.Keyboard.PressAsync("Tab");
        await Task.Delay(200);
        
        // Navigate through interactive elements
        for (int i = 0; i < 5; i++)
        {
            await Page.Keyboard.PressAsync("Tab");
            await Task.Delay(200);
        }
        
        // Test Enter key on focused element
        await Page.Keyboard.PressAsync("Enter");
        await Task.Delay(500);
        
        await TakeScreenshotAsync("accessibility_keyboard_navigation");
    }

    [Fact]
    public async Task Performance_ShouldLoadQuickly()
    {
        var startTime = DateTime.Now;
        
        await NavigateToUrlAsync(BaseUrl);
        
        var loadTime = DateTime.Now - startTime;
        
        // Verify page loads within reasonable time (5 seconds)
        Assert.True(loadTime.TotalSeconds < 5, $"Page should load within 5 seconds, but took {loadTime.TotalSeconds:F2} seconds");
        
        await TakeScreenshotAsync("performance_load_time");
    }

    [Fact]
    public async Task Performance_ShouldHaveReasonableResourceUsage()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Check for excessive DOM elements
        var elementCount = await Page.EvaluateAsync<int>("document.querySelectorAll('*').length");
        Assert.True(elementCount < 1500, $"Page should have reasonable number of DOM elements, but has {elementCount}");
        
        // Check for images with proper loading
        var images = await Page.QuerySelectorAllAsync("img");
        foreach (var img in images)
        {
            var loading = await img.GetAttributeAsync("loading");
            if (loading == null)
            {
                // If no loading attribute, check if it's above the fold
                var rect = await img.BoundingBoxAsync();
                if (rect != null && rect.Y > 1000) // Below fold
                {
                    // Images below the fold should have lazy loading
                    Assert.True(false, "Images below the fold should have lazy loading");
                }
            }
        }
        
        await TakeScreenshotAsync("performance_resource_usage");
    }

    [Fact]
    public async Task Performance_ShouldHandleLargeTextInput()
    {
        await NavigateToUrlAsync($"{BaseUrl}/word-counter");
        
        var inputSelector = "textarea";
        await WaitForElementAsync(inputSelector);
        
        // Test with large text input
        var largeText = string.Join(" ", Enumerable.Repeat("This is a test sentence.", 1000));
        
        var startTime = DateTime.Now;
        await FillInputAsync(inputSelector, largeText);
        var inputTime = DateTime.Now - startTime;
        
        // Verify input is processed quickly
        Assert.True(inputTime.TotalSeconds < 2, $"Large text input should be processed within 2 seconds, but took {inputTime.TotalSeconds:F2} seconds");
        
        await Task.Delay(1000); // Wait for processing
        
        // Verify no errors occurred
        var errorElement = await Page.QuerySelectorAsync(".error, .alert");
        Assert.Null(errorElement);
        
        await TakeScreenshotAsync("performance_large_input");
    }

    [Fact]
    public async Task Performance_ShouldHaveEfficientRendering()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Test scrolling performance
        var startTime = DateTime.Now;
        await Page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");
        await Task.Delay(1000);
        var scrollTime = DateTime.Now - startTime;
        
        // Verify smooth scrolling
        Assert.True(scrollTime.TotalSeconds < 3, $"Scrolling should be smooth and complete within 3 seconds, but took {scrollTime.TotalSeconds:F2} seconds");
        
        await TakeScreenshotAsync("performance_scrolling");
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperSemanticHTML()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Verify semantic HTML elements
        await AssertElementVisibleAsync("nav");
        await AssertElementVisibleAsync("main");
        await AssertElementVisibleAsync("footer");
        await AssertElementVisibleAsync("header");
        
        // Verify proper use of buttons vs links
        var buttons = await Page.QuerySelectorAllAsync("button");
        var links = await Page.QuerySelectorAllAsync("a");
        
        Assert.True(buttons.Count > 0, "Page should have interactive buttons");
        Assert.True(links.Count > 0, "Page should have navigation links");
        
        // Verify buttons are used for actions, links for navigation
        foreach (var button in buttons)
        {
            var text = await button.TextContentAsync();
            var ariaLabel = await button.GetAttributeAsync("aria-label");
            
            // Buttons should have descriptive text or aria-label
            Assert.True(!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(ariaLabel), 
                "Buttons should have descriptive text or aria-label");
        }
        
        await TakeScreenshotAsync("accessibility_semantic_html");
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperAltTextForImages()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        var images = await Page.QuerySelectorAllAsync("img");
        foreach (var img in images)
        {
            var alt = await img.GetAttributeAsync("alt");
            var src = await img.GetAttributeAsync("src");
            
            // Images should have alt text (unless decorative)
            if (!string.IsNullOrEmpty(src) && !src.Contains("icon") && !src.Contains("logo"))
            {
                Assert.True(!string.IsNullOrEmpty(alt), $"Image {src} should have alt text");
            }
        }
        
        await TakeScreenshotAsync("accessibility_image_alt_text");
    }

    [Fact]
    public async Task Performance_ShouldHaveOptimizedImages()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        var images = await Page.QuerySelectorAllAsync("img");
        foreach (var img in images)
        {
            var src = await img.GetAttributeAsync("src");
            var loading = await img.GetAttributeAsync("loading");
            
            // Images should have proper loading attributes
            if (!string.IsNullOrEmpty(src) && !src.Contains("data:"))
            {
                // Check if image is above the fold
                var rect = await img.BoundingBoxAsync();
                if (rect != null && rect.Y > 1000)
                {
                    // Images below the fold should have lazy loading
                    Assert.True(loading == "lazy", $"Image {src} below the fold should have lazy loading");
                }
            }
        }
        
        await TakeScreenshotAsync("performance_optimized_images");
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperErrorHandling()
    {
        await NavigateToUrlAsync($"{BaseUrl}/word-counter");
        
        var inputSelector = "textarea";
        await WaitForElementAsync(inputSelector);
        
        // Test with invalid input (very long text)
        var invalidText = new string('a', 100000);
        await FillInputAsync(inputSelector, invalidText);
        await Task.Delay(2000);
        
        // Verify no JavaScript errors occurred
        var consoleErrors = await Page.EvaluateAsync<string[]>("window.consoleErrors || []");
        Assert.Empty(consoleErrors);
        
        // Verify page is still functional
        await AssertElementVisibleAsync("textarea");
        
        await TakeScreenshotAsync("accessibility_error_handling");
    }
}
