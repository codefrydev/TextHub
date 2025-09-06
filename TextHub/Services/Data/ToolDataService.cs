using TextHub.Models;

namespace TextHub.Services.Data;

public class ToolDataService
{
    public List<Tool> GetFeaturedTools()
    {
        return new List<Tool>
        {
            new Tool("Uppercase Converter", "Convert any text to UPPERCASE instantly", "uppercase", "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" class=\"w-6 h-6 text-primary\"><polyline points=\"4 7 4 4 20 4 20 7\"></polyline><line x1=\"9\" x2=\"15\" y1=\"20\" y2=\"20\"></line><line x1=\"12\" x2=\"12\" y1=\"4\" y2=\"20\"></line></svg>"),
            new Tool("Word Counter", "Count words and characters in your text", "word-counter", "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" class=\"w-6 h-6 text-primary\"><line x1=\"4\" x2=\"20\" y1=\"9\" y2=\"9\"></line><line x1=\"4\" x2=\"20\" y1=\"15\" y2=\"15\"></line><line x1=\"10\" x2=\"8\" y1=\"3\" y2=\"21\"></line><line x1=\"16\" x2=\"14\" y1=\"3\" y2=\"21\"></line></svg>"),
            new Tool("Title Case", "Capitalize First Letter Of Each Word", "title-case", "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" class=\"w-6 h-6 text-primary\"><path d=\"m3 15 4-8 4 8\"></path><path d=\"M4 13h6\"></path><circle cx=\"18\" cy=\"12\" r=\"3\"></circle><path d=\"M21 9v6\"></path></svg>"),
            new Tool("Remove Extra Spaces", "Clean up unnecessary spaces from text", "remove-spaces", "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" class=\"w-6 h-6 text-primary\"><path d=\"M22 17v1c0 .5-.5 1-1 1H3c-.5 0-1-.5-1-1v-1\"></path></svg>")
        };
    }

    public List<Tool> GetTextCaseTools()
    {
        return new List<Tool>
        {
            new Tool("Uppercase Converter", "Convert any text to UPPERCASE instantly", "uppercase", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-type w-5 h-5 sm:w-6 sm:h-6 text-primary\"><polyline points=\"4 7 4 4 20 4 20 7\"></polyline><line x1=\"9\" x2=\"15\" y1=\"20\" y2=\"20\"></line><line x1=\"12\" x2=\"12\" y1=\"4\" y2=\"20\"></line></svg>"),
            new Tool("Lowercase Converter", "Transform text to lowercase with one click", "lowercase", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-letter-text w-5 h-5 sm:w-6 sm:h-6 text-primary\"><path d=\"M15 12h6\"></path><path d=\"M15 6h6\"></path><path d=\"m3 13 3.553-7.724a.5.5 0 0 1 .894 0L11 13\"></path><path d=\"M3 18h18\"></path><path d=\"M4 11h6\"></path></svg>"),
            new Tool("Title Case", "Capitalize First Letter Of Each Word", "title-case", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-case-sensitive w-5 h-5 sm:w-6 sm:h-6 text-primary\"><path d=\"m3 15 4-8 4 8\"></path><path d=\"M4 13h6\"></path><circle cx=\"18\" cy=\"12\" r=\"3\"></circle><path d=\"M21 9v6\"></path></svg>"),
            new Tool("Sentence Case", "Capitalize first letter of sentences", "sentence-case", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-case-sensitive w-5 h-5 sm:w-6 sm:h-6 text-primary\"><path d=\"m3 15 4-8 4 8\"></path><path d=\"M4 13h6\"></path><circle cx=\"18\" cy=\"12\" r=\"3\"></circle><path d=\"M21 9v6\"></path></svg>"),
            new Tool("camelCase", "Convert to camelCase for coding", "camel-case", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-code w-5 h-5 sm:w-6 sm:h-6 text-primary\"><polyline points=\"16 18 22 12 16 6\"></polyline><polyline points=\"8 6 2 12 8 18\"></polyline></svg>"),
            new Tool("PascalCase", "Convert to PascalCase for classes", "pascal-case", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-code w-5 h-5 sm:w-6 sm:h-6 text-primary\"><polyline points=\"16 18 22 12 16 6\"></polyline><polyline points=\"8 6 2 12 8 18\"></polyline></svg>"),
            new Tool("snake_case", "Convert to snake_case format", "snake-case", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-code w-5 h-5 sm:w-6 sm:h-6 text-primary\"><polyline points=\"16 18 22 12 16 6\"></polyline><polyline points=\"8 6 2 12 8 18\"></polyline></svg>"),
            new Tool("kebab-case", "Convert to kebab-case for URLs", "kebab-case", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-code w-5 h-5 sm:w-6 sm:h-6 text-primary\"><polyline points=\"16 18 22 12 16 6\"></polyline><polyline points=\"8 6 2 12 8 18\"></polyline></svg>")
        };
    }

    public List<Tool> GetTextAnalysisTools()
    {
        return new List<Tool>
        {
            new Tool("Word Counter", "Count words and characters in your text", "word-counter", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-hash w-6 h-6 text-primary\"><line x1=\"4\" x2=\"20\" y1=\"9\" y2=\"9\"></line><line x1=\"4\" x2=\"20\" y1=\"15\" y2=\"15\"></line><line x1=\"10\" x2=\"8\" y1=\"3\" y2=\"21\"></line><line x1=\"16\" x2=\"14\" y1=\"3\" y2=\"21\"></line></svg>"),
            new Tool("Character Counter", "Count total characters including spaces", "", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-file-text w-6 h-6 text-muted-foreground\"><path d=\"M15 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7Z\"></path><path d=\"M14 2v4a2 2 0 0 0 2 2h4\"></path><path d=\"M10 9H8\"></path><path d=\"M16 13H8\"></path><path d=\"M16 17H8\"></path></svg>", IsComingSoon: true),
            new Tool("Line Counter", "Count lines and paragraphs in text", "", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-file-text w-6 h-6 text-muted-foreground\"><path d=\"M15 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7Z\"></path><path d=\"M14 2v4a2 2 0 0 0 2 2h4\"></path><path d=\"M10 9H8\"></path><path d=\"M16 13H8\"></path><path d=\"M16 17H8\"></path></svg>", IsComingSoon: true)
        };
    }

    public List<Tool> GetTextFormattingTools()
    {
        return new List<Tool>
        {
            new Tool("Remove Extra Spaces", "Clean up unnecessary spaces from text", "remove-spaces", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-space w-6 h-6 text-primary\"><path d=\"M22 17v1c0 .5-.5 1-1 1H3c-.5 0-1-.5-1-1v-1\"></path></svg>"),
            new Tool("Remove Line Breaks", "Convert multi-line text to single line", "", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-scissors w-6 h-6 text-muted-foreground\"><circle cx=\"6\" cy=\"6\" r=\"3\"></circle><path d=\"M8.12 8.12 12 12\"></path><path d=\"M20 4 8.12 15.88\"></path><circle cx=\"6\" cy=\"18\" r=\"3\"></circle><path d=\"M14.8 14.8 20 20\"></path></svg>", IsComingSoon: true),
            new Tool("Find & Replace", "Search and replace text patterns", "", "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-replace w-6 h-6 text-muted-foreground\"><path d=\"M14 4a2 2 0 0 1 2-2\"></path><path d=\"M16 10a2 2 0 0 1-2-2\"></path><path d=\"M20 2a2 2 0 0 1 2 2\"></path><path d=\"M22 8a2 2 0 0 1-2 2\"></path><path d=\"m3 7 3 3 3-3\"></path><path d=\"M6 10V5a3 3 0 0 1 3-3h1\"></path><rect x=\"2\" y=\"14\" width=\"8\" height=\"8\" rx=\"2\"></rect></svg>", IsComingSoon: true)
        };
    }
}
