using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace TextHub.Tests;

[TestFixture]
public class TextFormattingToolsTests : PageTest
{
    private string BaseUrl => GlobalSetup.GetBaseUrl();

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Test]
    public async Task TextFormattingTools_ShouldBeVisible()
    {
        // Look for text formatting tools section
        var formattingSection = Page.Locator("[class*='formatting'], [class*='text-format']").First;
        await Expect(formattingSection).ToBeVisibleAsync();
    }

    [Test]
    public async Task TextFormattingTools_ShouldHaveFormattingOptions()
    {
        // Look for specific formatting tools
        var formattingTools = Page.Locator("a[href*='format'], a[href*='json'], a[href*='remove'], a[href*='find']");
        var toolCount = await formattingTools.CountAsync();
        
        Assert.That(toolCount, Is.GreaterThan(0), "Text formatting tools should be present");
    }

    [Test]
    public async Task TextFormattingTools_ShouldBeInteractive()
    {
        // Verify formatting tools are interactive
        var formattingLinks = Page.Locator("a[href*='format'], a[href*='json'], a[href*='remove']");
        var linkCount = await formattingLinks.CountAsync();
        
        if (linkCount > 0)
        {
            var firstLink = formattingLinks.First;
            await Expect(firstLink).ToBeVisibleAsync();
            await Expect(firstLink).ToBeEnabledAsync();
        }
    }
}
