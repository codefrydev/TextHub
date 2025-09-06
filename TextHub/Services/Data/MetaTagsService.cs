using TextHub.Models;

namespace TextHub.Services.Data;

public class MetaTagsService
{
    private const string BaseUrl = "https://codefrydev.in/TextHub";
    private const string SiteName = "Text Hub";
    private const string DefaultDescription = "Transform, analyze, and clean your text with our free online tools. 20+ text utilities including case converters, word counters, and formatting tools. No registration required.";
    
    public MetaTagsData GetHomePageMetaTags()
    {
        return new MetaTagsData
        {
            Title = "Text Hub - Quick & Simple Text Utilities",
            Description = DefaultDescription,
            Keywords = "text converter, case converter, uppercase, lowercase, title case, camel case, snake case, kebab case, word counter, character counter, text formatter, online text tools, free text utilities",
            CanonicalUrl = $"{BaseUrl}/",
            OgTitle = "Text Hub - Quick & Simple Text Utilities",
            OgDescription = DefaultDescription,
            OgImage = $"{BaseUrl}/icon-512.png",
            TwitterTitle = "Text Hub - Quick & Simple Text Utilities",
            TwitterDescription = DefaultDescription,
            TwitterImage = $"{BaseUrl}/icon-512.png"
        };
    }
    
    public MetaTagsData GetToolPageMetaTags(Tool tool, string category)
    {
        var toolUrl = $"{BaseUrl}{tool.Href}";
        var title = $"{tool.Title} - {SiteName}";
        var description = $"{tool.Description} | Free online {tool.Title.ToLower()} tool. {DefaultDescription}";
        var keywords = $"{tool.Title.ToLower()}, {category.ToLower()}, text converter, {tool.Description.ToLower()}, online text tools, free text utilities";
        
        return new MetaTagsData
        {
            Title = title,
            Description = description,
            Keywords = keywords,
            CanonicalUrl = toolUrl,
            OgTitle = title,
            OgDescription = description,
            OgImage = $"{BaseUrl}/icon-512.png",
            TwitterTitle = title,
            TwitterDescription = description,
            TwitterImage = $"{BaseUrl}/icon-512.png"
        };
    }
    
    public MetaTagsData GetCategoryPageMetaTags(string categoryName, string description)
    {
        var categoryUrl = $"{BaseUrl}/#{categoryName.ToLower().Replace(" ", "-")}";
        var title = $"{categoryName} Tools - {SiteName}";
        var metaDescription = $"{description} | Free online {categoryName.ToLower()} tools. {DefaultDescription}";
        var keywords = $"{categoryName.ToLower()}, text tools, text converter, online text utilities, free text tools";
        
        return new MetaTagsData
        {
            Title = title,
            Description = metaDescription,
            Keywords = keywords,
            CanonicalUrl = categoryUrl,
            OgTitle = title,
            OgDescription = metaDescription,
            OgImage = $"{BaseUrl}/icon-512.png",
            TwitterTitle = title,
            TwitterDescription = metaDescription,
            TwitterImage = $"{BaseUrl}/icon-512.png"
        };
    }
    
    public MetaTagsData GetDocumentationPageMetaTags()
    {
        var docUrl = $"{BaseUrl}/documentation";
        var title = $"Documentation - How to Contribute to {SiteName}";
        var description = "Learn how to contribute to TextHub and help improve our text utilities. Complete guide covering installation, project structure, adding new tools, and deployment.";
        var keywords = "text hub documentation, contribute, open source, text utilities, development guide, github";
        
        return new MetaTagsData
        {
            Title = title,
            Description = description,
            Keywords = keywords,
            CanonicalUrl = docUrl,
            OgTitle = title,
            OgDescription = description,
            OgImage = $"{BaseUrl}/icon-512.png",
            TwitterTitle = title,
            TwitterDescription = description,
            TwitterImage = $"{BaseUrl}/icon-512.png"
        };
    }
    
    public MetaTagsData GetSearchResultsMetaTags(string query, int resultCount)
    {
        var searchUrl = $"{BaseUrl}/?q={Uri.EscapeDataString(query)}";
        var title = $"Search Results for '{query}' - {SiteName}";
        var description = $"Found {resultCount} text tools matching '{query}'. {DefaultDescription}";
        var keywords = $"{query}, search results, text tools, text converter, online text utilities";
        
        return new MetaTagsData
        {
            Title = title,
            Description = description,
            Keywords = keywords,
            CanonicalUrl = searchUrl,
            OgTitle = title,
            OgDescription = description,
            OgImage = $"{BaseUrl}/icon-512.png",
            TwitterTitle = title,
            TwitterDescription = description,
            TwitterImage = $"{BaseUrl}/icon-512.png"
        };
    }
}

public class MetaTagsData
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string CanonicalUrl { get; set; } = string.Empty;
    public string OgTitle { get; set; } = string.Empty;
    public string OgDescription { get; set; } = string.Empty;
    public string OgImage { get; set; } = string.Empty;
    public string TwitterTitle { get; set; } = string.Empty;
    public string TwitterDescription { get; set; } = string.Empty;
    public string TwitterImage { get; set; } = string.Empty;
}
