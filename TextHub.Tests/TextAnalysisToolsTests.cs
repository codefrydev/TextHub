using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace TextHub.Tests;

[TestFixture]
public class TextAnalysisToolsTests : PageTest
{
    private string BaseUrl => GlobalSetup.GetBaseUrl();

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Test]
    public async Task TextAnalysisTools_ShouldBeVisible()
    {
        // Look for text analysis tools section
        var analysisSection = Page.Locator("[class*='analysis'], [class*='text-analysis']").First;
        await Expect(analysisSection).ToBeVisibleAsync();
    }

    [Test]
    public async Task TextAnalysisTools_ShouldHaveCounterTools()
    {
        // Look for specific analysis tools like word counter, character counter, etc.
        var counterTools = Page.Locator("a[href*='counter'], a[href*='count'], a[href*='word'], a[href*='character']");
        var toolCount = await counterTools.CountAsync();
        
        Assert.That(toolCount, Is.GreaterThan(0), "Text analysis counter tools should be present");
    }

    [Test]
    public async Task TextAnalysisTools_ShouldBeAccessible()
    {
        // Verify analysis tools are accessible and clickable
        var analysisLinks = Page.Locator("a[href*='analysis'], a[href*='counter'], a[href*='word']");
        var linkCount = await analysisLinks.CountAsync();
        
        if (linkCount > 0)
        {
            var firstLink = analysisLinks.First;
            await Expect(firstLink).ToBeVisibleAsync();
            await Expect(firstLink).ToBeEnabledAsync();
        }
    }
}
