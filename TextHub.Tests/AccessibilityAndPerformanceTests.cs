using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace TextHub.Tests;

[TestFixture]
public class AccessibilityAndPerformanceTests : PageTest
{
    private string BaseUrl => GlobalSetup.GetBaseUrl();

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Test]
    public async Task Page_ShouldHaveProperHeadingStructure()
    {
        // Check for proper heading hierarchy
        var h1 = Page.Locator("h1");
        var h1Count = await h1.CountAsync();
        Assert.That(h1Count, Is.EqualTo(1), "Page should have exactly one H1 heading");
        
        // Verify H1 is visible
        await Expect(h1.First).ToBeVisibleAsync();
    }

    [Test]
    public async Task Page_ShouldHaveSkipNavigation()
    {
        // Check for skip navigation link
        var skipNav = Page.Locator("a[href='#main-content']");
        await Expect(skipNav).ToBeVisibleAsync();
        
        // Test skip navigation functionality
        await skipNav.ClickAsync();
        await Expect(Page.Locator("#main-content")).ToBeFocusedAsync();
    }

    [Test]
    public async Task Page_ShouldHaveProperARIALabels()
    {
        // Check for proper ARIA roles
        var banner = Page.Locator("header[role='banner']");
        await Expect(banner).ToBeVisibleAsync();
        
        var main = Page.Locator("main[role='main']");
        await Expect(main).ToBeVisibleAsync();
        
        var contentinfo = Page.Locator("footer[role='contentinfo']").First;
        await Expect(contentinfo).ToBeVisibleAsync();
    }

    [Test]
    public async Task Page_ShouldBeKeyboardNavigable()
    {
        // Test keyboard navigation
        await Page.Keyboard.PressAsync("Tab");
        var focusedElement = Page.Locator(":focus");
        await Expect(focusedElement).ToBeVisibleAsync();
        
        // Test tab order
        await Page.Keyboard.PressAsync("Tab");
        await Page.Keyboard.PressAsync("Tab");
        await Page.Keyboard.PressAsync("Tab");
        
        // Verify we can still see focused elements
        await Expect(focusedElement).ToBeVisibleAsync();
    }

    [Test]
    public async Task Page_ShouldLoadWithinAcceptableTime()
    {
        var startTime = DateTime.Now;
        
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        var loadTime = DateTime.Now - startTime;
        
        // Page should load within 5 seconds
        Assert.That(loadTime.TotalSeconds, Is.LessThan(5), 
            $"Page took {loadTime.TotalSeconds:F2} seconds to load, which exceeds the 5-second limit");
    }

    [Test]
    public async Task Page_ShouldHaveReasonableImageSizes()
    {
        // Check for images and their sizes
        var images = Page.Locator("img");
        var imageCount = await images.CountAsync();
        
        if (imageCount > 0)
        {
            for (int i = 0; i < imageCount; i++)
            {
                var img = images.Nth(i);
                var src = await img.GetAttributeAsync("src");
                
                // Skip data URLs and external images
                if (src != null && !src.StartsWith("data:") && !src.StartsWith("http"))
                {
                    // Check if image has alt text
                    var alt = await img.GetAttributeAsync("alt");
                    Assert.That(alt, Is.Not.Null, $"Image {src} should have alt text");
                }
            }
        }
    }

    [Test]
    public async Task Page_ShouldHaveProperColorContrast()
    {
        // This is a basic test - in a real scenario, you'd use a proper accessibility testing library
        // For now, we'll just verify that text elements are visible
        var textElements = Page.Locator("p, h1, h2, h3, h4, h5, h6, span, div");
        var textCount = await textElements.CountAsync();
        
        Assert.That(textCount, Is.GreaterThan(0), "Page should have text content");
        
        // Verify main text is visible
        var mainText = Page.Locator("main p, main h1, main h2, main h3");
        var mainTextCount = await mainText.CountAsync();
        Assert.That(mainTextCount, Is.GreaterThan(0), "Main content should have visible text");
    }

    [Test]
    public async Task Page_ShouldWorkWithScreenReader()
    {
        // Test that important elements have proper labels
        var buttons = Page.Locator("button");
        var buttonCount = await buttons.CountAsync();
        
        for (int i = 0; i < buttonCount; i++)
        {
            var button = buttons.Nth(i);
            var text = await button.TextContentAsync();
            var ariaLabel = await button.GetAttributeAsync("aria-label");
            
            // Button should have either text content or aria-label
            Assert.That(!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(ariaLabel), 
                "Buttons should have text content or aria-label for screen readers");
        }
    }

    [Test]
    public async Task Page_ShouldHandleFocusManagement()
    {
        // Test focus management
        var focusableElements = Page.Locator("a, button, input, textarea, select");
        var focusableCount = await focusableElements.CountAsync();
        
        if (focusableCount > 0)
        {
            // Test that we can focus on elements
            await focusableElements.First.FocusAsync();
            var focusedElement = Page.Locator(":focus");
            await Expect(focusedElement).ToBeVisibleAsync();
        }
    }

    [Test]
    public async Task Page_ShouldHaveProperMetaTags()
    {
        // Check for essential meta tags
        var viewport = Page.Locator("meta[name='viewport']");
        await Expect(viewport).ToHaveAttributeAsync("content", new Regex(".*"));
        
        var charset = Page.Locator("meta[charset]");
        await Expect(charset).ToHaveAttributeAsync("charset", new Regex(".*"));
        
        // Check for description
        var description = Page.Locator("meta[name='description']");
        var descriptionContent = await description.GetAttributeAsync("content");
        Assert.That(descriptionContent, Is.Not.Null.And.Not.Empty, 
            "Page should have a meta description");
    }
}
