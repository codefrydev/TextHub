namespace TextHub.Tests;

public static class TestConfiguration
{
    public static class Urls
    {
        public const string Local = "https://localhost:5001";
        public const string Staging = "https://staging.texthub.com";
        public const string Production = "https://texthub.com";
    }

    public static class Timeouts
    {
        public const int Default = 30000; // 30 seconds
        public const int Long = 60000;    // 60 seconds
        public const int Short = 10000;   // 10 seconds
    }

    public static class Viewports
    {
        public static readonly ViewportSize Mobile = new(375, 667);
        public static readonly ViewportSize Tablet = new(768, 1024);
        public static readonly ViewportSize Desktop = new(1920, 1080);
        public static readonly ViewportSize LargeDesktop = new(2560, 1440);
    }

    public static class TestData
    {
        public const string SampleText = "Hello world! This is a sample text for testing purposes.";
        public const string LongText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
            "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
            "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " +
            "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla " +
            "pariatur. Excepteur sint occaecat cupidatat non proident, sunt in " +
            "culpa qui officia deserunt mollit anim id est laborum.";
        
        public const string JsonSample = "{\"name\":\"test\",\"value\":123,\"items\":[1,2,3]}";
        public const string MalformedJson = "{\"name\":\"test\",\"value\":123,\"items\":[1,2,3]";
    }

    public static string GetBaseUrl()
    {
        var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT")?.ToLower() ?? "local";
        
        return environment switch
        {
            "staging" => Urls.Staging,
            "production" => Urls.Production,
            _ => Urls.Local
        };
    }

    public static bool IsHeadless()
    {
        var headless = Environment.GetEnvironmentVariable("HEADLESS")?.ToLower();
        return headless == "true" || headless == "1";
    }

    public static int GetTimeout()
    {
        var timeout = Environment.GetEnvironmentVariable("TEST_TIMEOUT");
        return int.TryParse(timeout, out var result) ? result : Timeouts.Default;
    }
}

public record ViewportSize(int Width, int Height);
