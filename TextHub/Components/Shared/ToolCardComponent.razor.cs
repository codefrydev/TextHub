using Microsoft.AspNetCore.Components;
using TextHub.Models;

namespace TextHub.Components.Shared;

public partial class ToolCardComponent : ComponentBase
{
    [Parameter, EditorRequired]
    public Tool Tool { get; set; } = null!;

    [Parameter]
    public int AnimationDelay { get; set; } = 150;

    private static int _currentCardIndex = 0;
    private readonly string[] _availableGradientClasses = 
    [
        "bg-gradient-to-br from-primary/20 to-secondary/5",
        "bg-gradient-to-br from-secondary/20 to-primary/5", 
        "bg-gradient-to-br from-primary/20 to-primary/5",
        "bg-gradient-to-br from-secondary/20 to-secondary/5"
    ];

    private string GetGradientClass()
    {
        var selectedGradientClass = _availableGradientClasses[_currentCardIndex % _availableGradientClasses.Length];
        _currentCardIndex++;
        return selectedGradientClass;
    }
}