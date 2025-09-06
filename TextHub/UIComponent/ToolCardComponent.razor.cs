using Microsoft.AspNetCore.Components;
using TextHub.Models;

namespace TextHub.UIComponent;

public partial class ToolCardComponent : ComponentBase
{
    [Parameter, EditorRequired]
    public Tool Tool { get; set; } = null!;

    [Parameter]
    public int AnimationDelay { get; set; } = 150;

    private static int _cardIndex = 0;
    private readonly string[] _gradientClasses = 
    [
        "bg-gradient-to-br from-primary/20 to-secondary/5",
        "bg-gradient-to-br from-secondary/20 to-primary/5", 
        "bg-gradient-to-br from-primary/20 to-primary/5",
        "bg-gradient-to-br from-secondary/20 to-secondary/5"
    ];

    private string GetGradientClass()
    {
        var gradientClass = _gradientClasses[_cardIndex % _gradientClasses.Length];
        _cardIndex++;
        return gradientClass;
    }
}