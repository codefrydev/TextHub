using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace TextHub.Tests;

/// <summary>
/// Global setup for all Playwright tests
/// This class handles browser installation, server startup, and global test configuration
/// </summary>
[SetUpFixture]
public class GlobalSetup
{
    private static TestServerManager? _serverManager;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Console.WriteLine("Setting up global test environment...");
        
        // Install Playwright browsers
        Console.WriteLine("Installing Playwright browsers...");
        var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
        if (exitCode != 0)
        {
            throw new Exception($"Playwright install failed with exit code {exitCode}");
        }
        
        // Start the TextHub server
        Console.WriteLine("Starting TextHub server...");
        var projectPath = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "..", "TextHub"));
        _serverManager = new TestServerManager(projectPath);
        await _serverManager.StartAsync();
        
        Console.WriteLine("Global setup completed!");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("Global teardown - stopping TextHub server...");
        _serverManager?.Dispose();
        Console.WriteLine("Global teardown completed!");
    }

    /// <summary>
    /// Gets the base URL for the test server
    /// </summary>
    public static string GetBaseUrl()
    {
        return _serverManager?.BaseUrl ?? "https://localhost:5001";
    }
}
