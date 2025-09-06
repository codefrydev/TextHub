using System.Text;

namespace TextHub.Services.Converters;

public class TextCaseConverterService
{
    // Common words that should not be capitalized in title case (except at the beginning)
    private readonly HashSet<string> _lowercaseWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "a", "an", "and", "as", "at", "but", "by", "for", "if", "in", "nor", "of", "on", "or", "so", "the", "to", "up", "yet"
    };

    // Sentence ending punctuation marks
    private readonly char[] _sentenceEnders = { '.', '!', '?' };

    public string ConvertToUppercase(string input)
    {
        return string.IsNullOrWhiteSpace(input) ? string.Empty : input.ToUpper();
    }

    public string ConvertToLowercase(string input)
    {
        return string.IsNullOrWhiteSpace(input) ? string.Empty : input.ToLower();
    }

    public string ConvertToTitleCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var words = SplitIntoWords(input);
        if (words.Count == 0)
            return string.Empty;

        var result = new List<string>();

        for (int i = 0; i < words.Count; i++)
        {
            var word = words[i];
            
            // Skip empty words
            if (string.IsNullOrWhiteSpace(word))
            {
                result.Add(word);
                continue;
            }

            // Always capitalize the first word
            if (i == 0)
            {
                result.Add(CapitalizeWord(word));
            }
            // Always capitalize the last word
            else if (i == words.Count - 1)
            {
                result.Add(CapitalizeWord(word));
            }
            // For middle words, check if they should be lowercase
            else if (_lowercaseWords.Contains(word))
            {
                result.Add(word.ToLower());
            }
            else
            {
                result.Add(CapitalizeWord(word));
            }
        }

        return string.Join(" ", result);
    }

    public string ConvertToSentenceCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var result = new StringBuilder();
        var chars = input.ToCharArray();
        bool newSentence = true;

        for (int i = 0; i < chars.Length; i++)
        {
            char currentChar = chars[i];

            if (char.IsWhiteSpace(currentChar))
            {
                result.Append(currentChar);
                continue;
            }

            if (newSentence && char.IsLetter(currentChar))
            {
                result.Append(char.ToUpper(currentChar));
                newSentence = false;
            }
            else if (_sentenceEnders.Contains(currentChar))
            {
                result.Append(currentChar);
                newSentence = true;
            }
            else
            {
                result.Append(char.ToLower(currentChar));
            }
        }

        return result.ToString();
    }

    public string ConvertToCamelCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var words = SplitIntoWords(input);
        if (words.Count == 0)
            return string.Empty;

        var result = new StringBuilder();
        
        // First word is lowercase
        result.Append(words[0].ToLower());
        
        // Subsequent words are capitalized
        for (int i = 1; i < words.Count; i++)
        {
            result.Append(CapitalizeWord(words[i]));
        }

        return result.ToString();
    }

    public string ConvertToPascalCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var words = SplitIntoWords(input);
        if (words.Count == 0)
            return string.Empty;

        var result = new StringBuilder();
        
        // All words are capitalized
        foreach (var word in words)
        {
            result.Append(CapitalizeWord(word));
        }

        return result.ToString();
    }

    public string ConvertToSnakeCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var words = SplitIntoWords(input);
        if (words.Count == 0)
            return string.Empty;

        var result = new StringBuilder();
        
        // Join words with underscores, all lowercase
        for (int i = 0; i < words.Count; i++)
        {
            if (i > 0)
            {
                result.Append("_");
            }
            result.Append(words[i].ToLower());
        }

        return result.ToString();
    }

    public string ConvertToKebabCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var words = SplitIntoWords(input);
        if (words.Count == 0)
            return string.Empty;

        var result = new StringBuilder();
        
        // Join words with hyphens, all lowercase
        for (int i = 0; i < words.Count; i++)
        {
            if (i > 0)
            {
                result.Append("-");
            }
            result.Append(words[i].ToLower());
        }

        return result.ToString();
    }

    private List<string> SplitIntoWords(string text)
    {
        var words = new List<string>();
        var currentWord = new StringBuilder();
        
        foreach (char c in text)
        {
            if (char.IsLetterOrDigit(c))
            {
                currentWord.Append(c);
            }
            else if (char.IsWhiteSpace(c) || c == '-' || c == '_')
            {
                if (currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                }
            }
        }
        
        if (currentWord.Length > 0)
        {
            words.Add(currentWord.ToString());
        }
        
        return words;
    }

    private string CapitalizeWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return word;

        // Handle words with special characters or numbers
        var chars = word.ToCharArray();
        bool foundLetter = false;

        for (int i = 0; i < chars.Length; i++)
        {
            if (char.IsLetter(chars[i]))
            {
                if (!foundLetter)
                {
                    chars[i] = char.ToUpper(chars[i]);
                    foundLetter = true;
                }
                else
                {
                    chars[i] = char.ToLower(chars[i]);
                }
            }
        }

        return new string(chars);
    }
}
