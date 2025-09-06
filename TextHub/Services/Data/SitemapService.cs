using TextHub.Models;

namespace TextHub.Services.Data;

public class SitemapService
{
    private const string BaseUrl = "https://codefrydev.in/TextHub";
    private readonly ToolDataService _toolDataService;

    public SitemapService(ToolDataService toolDataService)
    {
        _toolDataService = toolDataService;
    }

    public string GenerateSitemap()
    {
        var tools = _toolDataService.GetAllTools();
        var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        
        var sitemap = $$"""
            <?xml version="1.0" encoding="UTF-8"?>
            <urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
                <url>
                    <loc>{{BaseUrl}}/</loc>
                    <lastmod>{{currentDate}}</lastmod>
                    <changefreq>daily</changefreq>
                    <priority>1.0</priority>
                </url>
                <url>
                    <loc>{{BaseUrl}}/documentation</loc>
                    <lastmod>{{currentDate}}</lastmod>
                    <changefreq>weekly</changefreq>
                    <priority>0.8</priority>
                </url>
            """;
        
        foreach (var tool in tools.Where(t => !t.IsComingSoon))
        {
            sitemap += $$"""
                
                <url>
                    <loc>{{BaseUrl}}{{tool.Href}}</loc>
                    <lastmod>{{currentDate}}</lastmod>
                    <changefreq>monthly</changefreq>
                    <priority>0.7</priority>
                </url>
            """;
        }
        
        sitemap += """
            
            </urlset>
            """;
        
        return sitemap;
    }

    public string GenerateRobotsTxt()
    {
        return $"""
            User-agent: *
            Allow: /

            Sitemap: {BaseUrl}/sitemap.xml

            # Crawl-delay for respectful crawling
            Crawl-delay: 1
            """;
    }
}