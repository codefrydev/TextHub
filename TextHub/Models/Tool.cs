namespace TextHub.Models;

// C# records for holding the tool data
public record Tool(string Title, string Description, string Href, string Icon, bool IsComingSoon = false);
public record ToolCategory(string Title, string Description, string CategoryIcon, List<Tool> Tools);