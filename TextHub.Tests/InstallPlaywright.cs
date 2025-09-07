using Microsoft.Playwright;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Installing Playwright browsers...");
        var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
        Console.WriteLine($"Installation completed with exit code: {exitCode}");
    }
}
