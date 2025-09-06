namespace TextHub.Models;

public class BreadcrumbItem
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}
