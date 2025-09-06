namespace TextHub.Models;

public record PageMetadata(
    string Title,
    string Description,
    string Url,
    string Type = "WebPage",
    string? ImageUrl = null,
    string? Author = null,
    DateTime? PublishedDate = null,
    DateTime? ModifiedDate = null,
    List<string>? Keywords = null,
    Dictionary<string, object>? AdditionalProperties = null
);

public record ToolPageMetadata(
    string Title,
    string Description,
    string Url,
    string ToolName,
    string Category,
    string? Instructions = null,
    List<string>? Features = null,
    Dictionary<string, object>? AdditionalProperties = null
) : PageMetadata(Title, Description, Url, "WebApplication", AdditionalProperties: AdditionalProperties);
