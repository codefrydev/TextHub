using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace TextHub.Tests;

[TestFixture]
public class TextCaseToolsTests : PageTest
{
    private string BaseUrl => GlobalSetup.GetBaseUrl();

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Test]
    public async Task TextCaseTools_ShouldBeVisible()
    {
        // Look for text case tools section
        var textCaseSection = Page.Locator("[class*='text-case'], [class*='case-tools']").First;
        await Expect(textCaseSection).ToBeVisibleAsync();
    }

    [Test]
    public async Task TextCaseTools_ShouldHaveMultipleTools()
    {
        // Look for tool cards or links related to text case conversion
        var toolElements = Page.Locator("a[href*='case'], [class*='tool'], [class*='card']");
        var toolCount = await toolElements.CountAsync();
        
        Assert.That(toolCount, Is.GreaterThan(0), "Text case tools should be present");
    }

    [Test]
    public async Task TextCaseTools_ShouldBeClickable()
    {
        // Find and test clicking on text case tool links
        var toolLinks = Page.Locator("a[href*='case'], a[href*='camel'], a[href*='pascal'], a[href*='snake'], a[href*='kebab']");
        var linkCount = await toolLinks.CountAsync();
        
        if (linkCount > 0)
        {
            var firstLink = toolLinks.First;
            await Expect(firstLink).ToBeVisibleAsync();
            await Expect(firstLink).ToBeEnabledAsync();
        }
    }
}
