using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace TextHub.Tests;

[TestFixture]
public class HomePageTests : PageTest
{
    private string BaseUrl => GlobalSetup.GetBaseUrl();

    [SetUp]
    public async Task SetUp()
    {
        // Navigate to the home page before each test
        await Page.GotoAsync(BaseUrl);
        
        // Wait for the page to load completely
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Test]
    public async Task HomePage_ShouldLoadSuccessfully()
    {
        // Verify the page title
        await Expect(Page).ToHaveTitleAsync("Text Hub - Quick & Simple Text Utilities");
        
        // Verify the main navigation is present
        await Expect(Page.Locator("header[role='banner']")).ToBeVisibleAsync();
        
        // Verify the main content area is present
        await Expect(Page.Locator("main[role='main']")).ToBeVisibleAsync();
        
        // Verify the footer is present
        await Expect(Page.Locator("footer[role='contentinfo']").First).ToBeVisibleAsync();
    }

    [Test]
    public async Task HomePage_ShouldDisplayHeroSection()
    {
        // Look for common hero section elements
        var heroSection = Page.Locator("main").First;
        await Expect(heroSection).ToBeVisibleAsync();
        
        // Verify the page loads without errors
        var title = await Page.TitleAsync();
        Assert.That(title, Is.EqualTo("Text Hub - Quick & Simple Text Utilities"));
    }

    [Test]
    public async Task HomePage_ShouldHaveSearchComponent()
    {
        // Look for search-related elements
        var searchElements = Page.Locator("input[type='search'], input[placeholder*='search'], .search, [class*='search']");
        var searchCount = await searchElements.CountAsync();
        
        // At least one search element should be present
        Assert.That(searchCount, Is.GreaterThan(0), "Search component should be present on the home page");
    }

    [Test]
    public async Task HomePage_ShouldDisplayToolSections()
    {
        // Verify various tool sections are present
        var mainContent = Page.Locator("main").First;
        await Expect(mainContent).ToBeVisibleAsync();
        
        // Check for section headers or tool-related content
        var sections = Page.Locator("section, [class*='section'], [class*='tool']");
        var sectionCount = await sections.CountAsync();
        
        Assert.That(sectionCount, Is.GreaterThan(0), "Tool sections should be present on the home page");
    }

    [Test]
    public async Task Navigation_ShouldBeAccessible()
    {
        // Check for skip navigation link
        var skipNav = Page.Locator("a[href='#main-content']").First;
        await Expect(skipNav).ToBeVisibleAsync();
        
        // Verify navigation structure
        var nav = Page.Locator("nav, [role='navigation']");
        var navCount = await nav.CountAsync();
        Assert.That(navCount, Is.GreaterThan(0), "Navigation should be present");
    }

    [Test]
    public async Task Page_ShouldBeResponsive()
    {
        // Test mobile viewport
        await Page.SetViewportSizeAsync(375, 667);
        await Page.ReloadAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify main content is still visible
        await Expect(Page.Locator("main").First).ToBeVisibleAsync();
        
        // Test tablet viewport
        await Page.SetViewportSizeAsync(768, 1024);
        await Page.ReloadAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify main content is still visible
        await Expect(Page.Locator("main").First).ToBeVisibleAsync();
        
        // Test desktop viewport
        await Page.SetViewportSizeAsync(1920, 1080);
        await Page.ReloadAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify main content is still visible
        await Expect(Page.Locator("main").First).ToBeVisibleAsync();
    }

    [Test]
    public async Task Page_ShouldHaveProperMetaTags()
    {
        // Check for meta description
        var metaDescription = Page.Locator("meta[name='description']").First;
        await Expect(metaDescription).ToHaveAttributeAsync("content", new Regex(".*"));
        
        // Check for viewport meta tag
        var viewport = Page.Locator("meta[name='viewport']").First;
        await Expect(viewport).ToHaveAttributeAsync("content", new Regex(".*"));
    }

    [Test]
    public async Task Page_ShouldLoadWithoutConsoleErrors()
    {
        var consoleMessages = new List<string>();
        
        Page.Console += (_, msg) =>
        {
            if (msg.Type == "error")
            {
                consoleMessages.Add(msg.Text);
            }
        };
        
        await Page.ReloadAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Filter out common non-critical errors
        var criticalErrors = consoleMessages.Where(msg => 
            !msg.Contains("favicon") && 
            !msg.Contains("service-worker") &&
            !msg.Contains("manifest") &&
            !msg.Contains("X-Frame-Options")
        ).ToList();
        
        Assert.That(criticalErrors, Is.Empty, 
            $"Page should not have console errors. Found: {string.Join(", ", criticalErrors)}");
    }
}
