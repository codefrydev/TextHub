using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace TextHub.Tests;

[SetUpFixture]
public class PlaywrightFixture
{
    [OneTimeSetUp]
    public void SetUp()
    {
        // Install Playwright browsers if not already installed
        Microsoft.Playwright.Program.Main(new[] { "install" });
    }
}

[Parallelizable(ParallelScope.Self)]
public class PlaywrightTest : PageTest
{
    [SetUp]
    public async Task SetUp()
    {
        // Configure browser context options
        await Page.SetViewportSizeAsync(1920, 1080);
        
        // Set up console message handling
        Page.Console += (_, msg) =>
        {
            if (msg.Type == "error")
            {
                Console.WriteLine($"Console Error: {msg.Text}");
            }
        };
        
        // Set up page error handling
        Page.PageError += (_, error) =>
        {
            Console.WriteLine($"Page Error: {error}");
        };
    }
    
    // TearDown is handled by the base PageTest class
}

// This file contains only the SetUpFixture for global setup
