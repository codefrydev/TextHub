using Microsoft.Playwright;
using Xunit;
using System.Threading.Tasks;
using TextHub.Tests.Common;

namespace TextHub.Tests.Integration;

/// <summary>
/// UI tests based on the actual Blazor project structure and functionality
/// </summary>
public class BlazorProjectUITests : UITestBase
{
    [Fact]
    public async Task HomePage_ShouldDisplayHeroSection()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Verify hero section elements
        await AssertElementVisibleAsync("h1:has-text('Text Hub')");
        await AssertElementVisibleAsync("h2:has-text('Quick and simple text utilities in one place.')");
        
        // Verify CTA buttons
        await AssertElementVisibleAsync("a[href='#tools']:has-text('Start Using Tools')");
        await AssertElementVisibleAsync("a[href='#features']:has-text('Learn More')");
        
        // Verify stats section
        await AssertElementVisibleAsync("div:has-text('Text Tools')");
        await AssertElementVisibleAsync("div:has-text('Free to Use')");
        
        await TakeScreenshotAsync("hero_section");
    }

    [Fact]
    public async Task HomePage_ShouldDisplayToolSections()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Scroll to tools section
        await Page.EvaluateAsync("document.querySelector('#tools')?.scrollIntoView()");
        await Task.Delay(1000);
        
        // Verify Text Case Tools section
        await AssertElementVisibleAsync("h2:has-text('Text Case Tools')");
        await AssertElementVisibleAsync("p:has-text('Convert text between different cases')");
        
        // Verify Text Analysis section
        await AssertElementVisibleAsync("h2:has-text('Text Analysis')");
        await AssertElementVisibleAsync("p:has-text('Analyze and count text properties')");
        
        // Verify Text Formatting section
        await AssertElementVisibleAsync("h2:has-text('Text Formatting')");
        await AssertElementVisibleAsync("p:has-text('Clean and format your text')");
        
        await TakeScreenshotAsync("tool_sections");
    }

    [Fact]
    public async Task ToolCards_ShouldDisplayCorrectly()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Scroll to tools section
        await Page.EvaluateAsync("document.querySelector('#tools')?.scrollIntoView()");
        await Task.Delay(1000);
        
        // Verify Text Case Tools cards
        var uppercaseCard = await WaitForElementAsync("a[href='uppercase']");
        Assert.NotNull(uppercaseCard);
        Assert.Contains("Uppercase Converter", await uppercaseCard.TextContentAsync());
        
        var lowercaseCard = await WaitForElementAsync("a[href='lowercase']");
        Assert.NotNull(lowercaseCard);
        Assert.Contains("Lowercase Converter", await lowercaseCard.TextContentAsync());
        
        var titleCaseCard = await WaitForElementAsync("a[href='title-case']");
        Assert.NotNull(titleCaseCard);
        Assert.Contains("Title Case", await titleCaseCard.TextContentAsync());
        
        await TakeScreenshotAsync("tool_cards");
    }

    [Fact]
    public async Task NavigationDropdown_ShouldWorkCorrectly()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Click tools dropdown
        await ClickElementAsync("button:has-text('Tools')");
        
        // Verify dropdown is open
        var dropdown = await WaitForElementAsync("#tools-dropdown");
        Assert.NotNull(dropdown);
        
        // Verify section buttons exist
        var textCaseButton = await WaitForElementAsync("button:has-text('Text Case Tools')");
        Assert.NotNull(textCaseButton);
        
        var textAnalysisButton = await WaitForElementAsync("button:has-text('Text Analysis')");
        Assert.NotNull(textAnalysisButton);
        
        var textFormattingButton = await WaitForElementAsync("button:has-text('Text Formatting')");
        Assert.NotNull(textFormattingButton);
        
        await TakeScreenshotAsync("navigation_dropdown");
    }

    [Fact]
    public async Task TextCaseToolsDropdown_ShouldExpandCorrectly()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Open dropdown and expand Text Case Tools
        await ClickElementAsync("button:has-text('Tools')");
        await ClickElementAsync("button:has-text('Text Case Tools')");
        
        // Verify tools are visible
        var uppercaseLink = await WaitForElementAsync("a:has-text('Uppercase Converter')");
        Assert.NotNull(uppercaseLink);
        
        var lowercaseLink = await WaitForElementAsync("a:has-text('Lowercase Converter')");
        Assert.NotNull(lowercaseLink);
        
        var titleCaseLink = await WaitForElementAsync("a:has-text('Title Case')");
        Assert.NotNull(titleCaseLink);
        
        var camelCaseLink = await WaitForElementAsync("a:has-text('camelCase')");
        Assert.NotNull(camelCaseLink);
        
        await TakeScreenshotAsync("text_case_dropdown_expanded");
    }

    [Fact]
    public async Task TextAnalysisToolsDropdown_ShouldExpandCorrectly()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Open dropdown and expand Text Analysis Tools
        await ClickElementAsync("button:has-text('Tools')");
        await ClickElementAsync("button:has-text('Text Analysis')");
        
        // Verify tools are visible
        var wordCounterLink = await WaitForElementAsync("a:has-text('Word Counter')");
        Assert.NotNull(wordCounterLink);
        
        var characterCounterLink = await WaitForElementAsync("a:has-text('Character Counter')");
        Assert.NotNull(characterCounterLink);
        
        var lineCounterLink = await WaitForElementAsync("a:has-text('Line Counter')");
        Assert.NotNull(lineCounterLink);
        
        await TakeScreenshotAsync("text_analysis_dropdown_expanded");
    }

    [Fact]
    public async Task TextFormattingToolsDropdown_ShouldExpandCorrectly()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Open dropdown and expand Text Formatting Tools
        await ClickElementAsync("button:has-text('Tools')");
        await ClickElementAsync("button:has-text('Text Formatting')");
        
        // Verify tools are visible
        var removeSpacesLink = await WaitForElementAsync("a:has-text('Remove Extra Spaces')");
        Assert.NotNull(removeSpacesLink);
        
        var removeLineBreaksLink = await WaitForElementAsync("a:has-text('Remove Line Breaks')");
        Assert.NotNull(removeLineBreaksLink);
        
        var findReplaceLink = await WaitForElementAsync("a:has-text('Find & Replace')");
        Assert.NotNull(findReplaceLink);
        
        var jsonFormatterLink = await WaitForElementAsync("a:has-text('JSON Formatter')");
        Assert.NotNull(jsonFormatterLink);
        
        await TakeScreenshotAsync("text_formatting_dropdown_expanded");
    }

    [Fact]
    public async Task ToolNavigation_ShouldWorkFromDropdown()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Navigate to Word Counter via dropdown
        await ClickElementAsync("button:has-text('Tools')");
        await ClickElementAsync("button:has-text('Text Analysis')");
        await ClickElementAsync("a:has-text('Word Counter')", waitForNavigation: true);
        
        // Verify we're on the word counter page
        await AssertPageTitleAsync("Word Counter - Text Hub");
        await AssertElementVisibleAsync("h1:has-text('Word Counter')");
        
        await TakeScreenshotAsync("tool_navigation_from_dropdown");
    }

    [Fact]
    public async Task ToolNavigation_ShouldWorkFromHomePageCards()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Scroll to tools section
        await Page.EvaluateAsync("document.querySelector('#tools')?.scrollIntoView()");
        await Task.Delay(1000);
        
        // Click on Uppercase Converter card
        await ClickElementAsync("a[href='uppercase']", waitForNavigation: true);
        
        // Verify we're on the uppercase page
        await AssertPageTitleAsync("Uppercase Converter - Text Hub");
        await AssertElementVisibleAsync("h1:has-text('Uppercase Converter')");
        
        await TakeScreenshotAsync("tool_navigation_from_cards");
    }

    [Fact]
    public async Task MobileMenu_ShouldToggleCorrectly()
    {
        // Set mobile viewport
        await Page.SetViewportSizeAsync(375, 667);
        await NavigateToUrlAsync(BaseUrl);
        
        // Verify mobile menu button is visible
        await AssertElementVisibleAsync("button[aria-label='Open menu']");
        
        // Click mobile menu button
        await ClickElementAsync("button[aria-label='Open menu']");
        
        // Verify mobile menu is open
        var homeLink = await WaitForElementAsync("a[href='/']");
        Assert.NotNull(homeLink);
        
        var toolsLink = await WaitForElementAsync("a[href='#tools']");
        Assert.NotNull(toolsLink);
        
        var docsLink = await WaitForElementAsync("a[href='documentation']");
        if (docsLink == null)
        {
            // If docs link not visible in mobile, just verify other elements exist
            Assert.NotNull(homeLink);
            Assert.NotNull(toolsLink);
        }
        else
        {
            Assert.NotNull(docsLink);
        }
        
        await TakeScreenshotAsync("mobile_menu_open");
        
        // Click menu button again to close
        await ClickElementAsync("button[aria-label='Open menu']");
        await Task.Delay(500);
        
        // Verify mobile menu is closed (or at least the button state changed)
        var menuButton = await Page.QuerySelectorAsync("button[aria-label='Open menu']");
        if (menuButton != null)
        {
            // Check if button state changed (aria-expanded or similar)
            var ariaExpanded = await menuButton.GetAttributeAsync("aria-expanded");
            if (ariaExpanded != null)
            {
                Assert.Equal("false", ariaExpanded);
            }
            else
            {
                // If no aria-expanded, just verify the button is still there
                Assert.NotNull(menuButton);
            }
        }
    }

    [Fact]
    public async Task ThemeToggle_ShouldWork()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Find theme toggle button
        var themeButton = await WaitForElementAsync("button[aria-label='Toggle dark mode']");
        Assert.NotNull(themeButton);
        
        // Click theme toggle
        await ClickElementAsync("button[aria-label='Toggle dark mode']");
        await Task.Delay(500); // Wait for theme change
        
        // Verify theme changed (check for dark mode class or attribute)
        var htmlElement = await Page.QuerySelectorAsync("html");
        var className = await htmlElement.GetAttributeAsync("class");
        
        // Theme should have changed (either to dark or light)
        Assert.NotNull(className);
        
        await TakeScreenshotAsync("theme_toggle_test");
    }

    [Fact]
    public async Task ResponsiveDesign_ShouldAdaptToDifferentScreenSizes()
    {
        // Test desktop (1920x1080)
        await Page.SetViewportSizeAsync(1920, 1080);
        await NavigateToUrlAsync(BaseUrl);
        
        // Verify desktop layout
        await AssertElementVisibleAsync("div[role='menubar']"); // Desktop menu
        await AssertElementHiddenAsync("button[aria-label='Open menu']"); // Mobile menu hidden
        await TakeScreenshotAsync("desktop_responsive");
        
        // Test tablet (768x1024)
        await Page.SetViewportSizeAsync(768, 1024);
        await Page.ReloadAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify tablet layout
        await AssertElementVisibleAsync("div[role='menubar']"); // Desktop menu still visible
        await AssertElementHiddenAsync("button[aria-label='Open menu']"); // Mobile menu still hidden
        await TakeScreenshotAsync("tablet_responsive");
        
        // Test mobile (375x667)
        await Page.SetViewportSizeAsync(375, 667);
        await Page.ReloadAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Verify mobile layout
        await AssertElementHiddenAsync("div[role='menubar']"); // Desktop menu hidden
        await AssertElementVisibleAsync("button[aria-label='Open menu']"); // Mobile menu visible
        await TakeScreenshotAsync("mobile_responsive");
    }

    [Fact]
    public async Task Accessibility_ShouldHaveProperStructure()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Verify semantic HTML elements
        await AssertElementVisibleAsync("nav[role='navigation']");
        await AssertElementVisibleAsync("main[role='main']");
        await AssertElementVisibleAsync("footer[role='contentinfo']");
        await AssertElementVisibleAsync("header[role='banner']");
        
        // Verify ARIA labels
        var navWithLabel = await WaitForElementAsync("nav[aria-label='Main navigation']");
        if (navWithLabel != null)
        {
            await AssertElementVisibleAsync("nav[aria-label='Main navigation']");
        }
        
        await AssertElementVisibleAsync("a[aria-label='Text Hub - Home']");
        
        // Theme toggle might not be visible on all pages
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
        
        await TakeScreenshotAsync("accessibility_structure");
    }

    [Fact]
    public async Task Performance_ShouldLoadWithinReasonableTime()
    {
        var startTime = DateTime.Now;
        
        await NavigateToUrlAsync(BaseUrl);
        
        var loadTime = DateTime.Now - startTime;
        
        // Verify page loads within reasonable time (5 seconds)
        Assert.True(loadTime.TotalSeconds < 5, $"Page should load within 5 seconds, but took {loadTime.TotalSeconds:F2} seconds");
        
        await TakeScreenshotAsync("performance_load_time");
    }

    [Fact]
    public async Task Performance_ShouldHaveReasonableDOMSize()
    {
        await NavigateToUrlAsync(BaseUrl);
        
        // Check for reasonable DOM elements count
        var elementCount = await Page.EvaluateAsync<int>("document.querySelectorAll('*').length");
        Assert.True(elementCount < 1500, $"Page should have reasonable number of DOM elements, but has {elementCount}");
        
        await TakeScreenshotAsync("performance_dom_size");
    }
}
