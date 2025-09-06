using System.Text.Json;
using TextHub.Models;

namespace TextHub.Services.Data;

public class JsonLdService
{
    private const string BaseUrl = "https://codefrydev.in/TextHub";
    
    public string GenerateHomePageJsonLd()
    {
        var json = $$"""
            {
                "@context": "https://schema.org",
                "@graph": [
                    {
                        "@type": "WebSite",
                        "@id": "{{BaseUrl}}/#website",
                        "url": "{{BaseUrl}}/",
                        "name": "Text Hub - Quick & Simple Text Utilities",
                        "description": "Transform, analyze, and clean your text with our free online tools. 20+ text utilities including case converters, word counters, and formatting tools. No registration required.",
                        "publisher": {
                            "@id": "{{BaseUrl}}/#organization"
                        },
                        "potentialAction": {
                            "@type": "SearchAction",
                            "target": {
                                "@type": "EntryPoint",
                                "urlTemplate": "{{BaseUrl}}/?q={search_term_string}"
                            },
                            "query-input": "required name=search_term_string"
                        },
                        "inLanguage": "en-US"
                    },
                    {
                        "@type": "SoftwareApplication",
                        "@id": "{{BaseUrl}}/#software",
                        "name": "Text Hub",
                        "description": "A comprehensive collection of free online text utilities for case conversion, text analysis, and formatting. Features 20+ tools including uppercase/lowercase converters, word counters, title case, camelCase, snake_case, and more.",
                        "url": "{{BaseUrl}}/",
                        "applicationCategory": "WebApplication",
                        "operatingSystem": "Any",
                        "browserRequirements": "Requires JavaScript. Requires HTML5.",
                        "softwareVersion": "1.0",
                        "datePublished": "2024-01-01",
                        "dateModified": "{{DateTime.Now:yyyy-MM-dd}}",
                        "offers": {
                            "@type": "Offer",
                            "price": "0",
                            "priceCurrency": "USD"
                        },
                        "featureList": [
                            "Uppercase Converter", "Lowercase Converter", "Title Case Converter",
                            "Sentence Case Converter", "camelCase Converter", "PascalCase Converter",
                            "snake_case Converter", "kebab-case Converter", "Word Counter",
                            "Character Counter", "Line Counter", "Remove Extra Spaces",
                            "Remove Line Breaks", "Find & Replace"
                        ],
                        "author": {
                            "@id": "{{BaseUrl}}/#organization"
                        },
                        "publisher": {
                            "@id": "{{BaseUrl}}/#organization"
                        }
                    },
                    {
                        "@type": "Organization",
                        "@id": "{{BaseUrl}}/#organization",
                        "name": "Text Hub",
                        "url": "{{BaseUrl}}/",
                        "logo": {
                            "@type": "ImageObject",
                            "url": "{{BaseUrl}}/icon-512.png",
                            "width": 512,
                            "height": 512
                        },
                        "sameAs": [],
                        "contactPoint": {
                            "@type": "ContactPoint",
                            "contactType": "customer service",
                            "availableLanguage": "English"
                        }
                    },
                    {
                        "@type": "WebPage",
                        "@id": "{{BaseUrl}}/#webpage",
                        "url": "{{BaseUrl}}/",
                        "name": "Text Hub - Quick & Simple Text Utilities",
                        "isPartOf": {
                            "@id": "{{BaseUrl}}/#website"
                        },
                        "about": {
                            "@id": "{{BaseUrl}}/#software"
                        },
                        "description": "Transform, analyze, and clean your text with our free online tools. No registration required.",
                        "breadcrumb": {
                            "@type": "BreadcrumbList",
                            "itemListElement": [
                                {
                                    "@type": "ListItem",
                                    "position": 1,
                                    "name": "Home",
                                    "item": "{{BaseUrl}}/"
                                }
                            ]
                        },
                        "inLanguage": "en-US"
                    }
                ]
            }
            """;

        return json;
    }

    public string GenerateToolPageJsonLd(Tool tool, string category)
    {
        var toolUrl = $"{BaseUrl}{tool.Href}";
        var toolId = $"{BaseUrl}{tool.Href}#tool";
        
        var json = $$"""
            {
                "@context": "https://schema.org",
                "@graph": [
                    {
                        "@type": "WebApplication",
                        "@id": "{{toolId}}",
                        "name": "{{tool.Title}}",
                        "description": "{{tool.Description}}",
                        "url": "{{toolUrl}}",
                        "applicationCategory": "WebApplication",
                        "operatingSystem": "Any",
                        "browserRequirements": "Requires JavaScript. Requires HTML5.",
                        "offers": {
                            "@type": "Offer",
                            "price": "0",
                            "priceCurrency": "USD"
                        },
                        "author": {
                            "@id": "{{BaseUrl}}/#organization"
                        },
                        "publisher": {
                            "@id": "{{BaseUrl}}/#organization"
                        },
                        "isPartOf": {
                            "@id": "{{BaseUrl}}/#software"
                        },
                        "about": {
                            "@id": "{{BaseUrl}}/#software"
                        }
                    },
                    {
                        "@type": "WebPage",
                        "@id": "{{toolUrl}}#webpage",
                        "url": "{{toolUrl}}",
                        "name": "{{tool.Title}} - {{tool.Description}}",
                        "description": "{{tool.Description}}",
                        "isPartOf": {
                            "@id": "{{BaseUrl}}/#website"
                        },
                        "about": {
                            "@id": "{{toolId}}"
                        },
                        "breadcrumb": {
                            "@type": "BreadcrumbList",
                            "itemListElement": [
                                {
                                    "@type": "ListItem",
                                    "position": 1,
                                    "name": "Home",
                                    "item": "{{BaseUrl}}/"
                                },
                                {
                                    "@type": "ListItem",
                                    "position": 2,
                                    "name": "{{category}}",
                                    "item": "{{BaseUrl}}/#{{category.ToLower().Replace(" ", "-")}}"
                                },
                                {
                                    "@type": "ListItem",
                                    "position": 3,
                                    "name": "{{tool.Title}}",
                                    "item": "{{toolUrl}}"
                                }
                            ]
                        },
                        "inLanguage": "en-US"
                    },
                    {
                        "@type": "HowTo",
                        "name": "How to use {{tool.Title}}",
                        "description": "{{tool.Description}}",
                        "step": [
                            {
                                "@type": "HowToStep",
                                "name": "Enter your text",
                                "text": "Paste or type your text into the input field"
                            },
                            {
                                "@type": "HowToStep",
                                "name": "Process text",
                                "text": "Click the convert button to {{tool.Description.ToLower()}}"
                            },
                            {
                                "@type": "HowToStep",
                                "name": "Copy result",
                                "text": "Copy the processed text to your clipboard"
                            }
                        ],
                        "totalTime": "PT1M",
                        "supply": [
                            {
                                "@type": "HowToSupply",
                                "name": "Text input"
                            }
                        ],
                        "tool": [
                            {
                                "@type": "HowToTool",
                                "name": "{{tool.Title}}",
                                "url": "{{toolUrl}}"
                            }
                        ]
                    }
                ]
            }
            """;

        return json;
    }

    public string GenerateCategoryPageJsonLd(string categoryName, string description, List<Tool> tools)
    {
        var categoryUrl = $"{BaseUrl}/#{categoryName.ToLower().Replace(" ", "-")}";
        var categoryId = $"{BaseUrl}/#{categoryName.ToLower().Replace(" ", "-")}#category";
        
        var toolItems = string.Join(",", tools.Select((tool, index) => $$"""
            {
                "@type": "ListItem",
                "position": {{index + 1}},
                "item": {
                    "@type": "WebApplication",
                    "name": "{{tool.Title}}",
                    "description": "{{tool.Description}}",
                    "url": "{{BaseUrl}}{{tool.Href}}"
                }
            }
            """));
        
        var json = $$"""
            {
                "@context": "https://schema.org",
                "@graph": [
                    {
                        "@type": "CollectionPage",
                        "@id": "{{categoryId}}",
                        "name": "{{categoryName}} Tools",
                        "description": "{{description}}",
                        "url": "{{categoryUrl}}",
                        "isPartOf": {
                            "@id": "{{BaseUrl}}/#website"
                        },
                        "mainEntity": {
                            "@type": "ItemList",
                            "name": "{{categoryName}} Tools",
                            "description": "{{description}}",
                            "numberOfItems": {{tools.Count}},
                            "itemListElement": [{{toolItems}}]
                        },
                        "breadcrumb": {
                            "@type": "BreadcrumbList",
                            "itemListElement": [
                                {
                                    "@type": "ListItem",
                                    "position": 1,
                                    "name": "Home",
                                    "item": "{{BaseUrl}}/"
                                },
                                {
                                    "@type": "ListItem",
                                    "position": 2,
                                    "name": "{{categoryName}}",
                                    "item": "{{categoryUrl}}"
                                }
                            ]
                        },
                        "inLanguage": "en-US"
                    }
                ]
            }
            """;

        return json;
    }

    public string GenerateSearchResultsJsonLd(string query, List<Tool> results)
    {
        var searchUrl = $"{BaseUrl}/?q={Uri.EscapeDataString(query)}";
        
        var resultItems = string.Join(",", results.Select((tool, index) => $$"""
            {
                "@type": "ListItem",
                "position": {{index + 1}},
                "item": {
                    "@type": "WebApplication",
                    "name": "{{tool.Title}}",
                    "description": "{{tool.Description}}",
                    "url": "{{BaseUrl}}{{tool.Href}}"
                }
            }
            """));
        
        var json = $$"""
            {
                "@context": "https://schema.org",
                "@graph": [
                    {
                        "@type": "SearchResultsPage",
                        "name": "Search results for '{{query}}'",
                        "description": "Found {{results.Count}} text tools matching '{{query}}'",
                        "url": "{{searchUrl}}",
                        "isPartOf": {
                            "@id": "{{BaseUrl}}/#website"
                        },
                        "mainEntity": {
                            "@type": "ItemList",
                            "name": "Search results for '{{query}}'",
                            "numberOfItems": {{results.Count}},
                            "itemListElement": [{{resultItems}}]
                        },
                        "breadcrumb": {
                            "@type": "BreadcrumbList",
                            "itemListElement": [
                                {
                                    "@type": "ListItem",
                                    "position": 1,
                                    "name": "Home",
                                    "item": "{{BaseUrl}}/"
                                },
                                {
                                    "@type": "ListItem",
                                    "position": 2,
                                    "name": "Search: {{query}}",
                                    "item": "{{searchUrl}}"
                                }
                            ]
                        },
                        "inLanguage": "en-US"
                    }
                ]
            }
            """;

        return json;
    }

    public string GenerateDocumentationPageJsonLd()
    {
        var docUrl = $"{BaseUrl}/documentation";
        var docId = $"{BaseUrl}/documentation#documentation";
        
        var json = $$"""
            {
                "@context": "https://schema.org",
                "@graph": [
                    {
                        "@type": "WebPage",
                        "@id": "{{docId}}",
                        "url": "{{docUrl}}",
                        "name": "Documentation - How to Contribute to TextHub",
                        "description": "Learn how to contribute to TextHub and help improve our text utilities. Complete guide covering installation, project structure, adding new tools, and deployment.",
                        "isPartOf": {
                            "@id": "{{BaseUrl}}/#website"
                        },
                        "about": {
                            "@id": "{{BaseUrl}}/#software"
                        },
                        "breadcrumb": {
                            "@type": "BreadcrumbList",
                            "itemListElement": [
                                {
                                    "@type": "ListItem",
                                    "position": 1,
                                    "name": "Home",
                                    "item": "{{BaseUrl}}/"
                                },
                                {
                                    "@type": "ListItem",
                                    "position": 2,
                                    "name": "Documentation",
                                    "item": "{{docUrl}}"
                                }
                            ]
                        },
                        "inLanguage": "en-US",
                        "dateModified": "{{DateTime.Now:yyyy-MM-dd}}",
                        "author": {
                            "@id": "{{BaseUrl}}/#organization"
                        },
                        "publisher": {
                            "@id": "{{BaseUrl}}/#organization"
                        }
                    },
                    {
                        "@type": "TechArticle",
                        "@id": "{{docId}}#techarticle",
                        "headline": "How to Contribute to TextHub",
                        "description": "Complete documentation for contributing to the TextHub project, including setup instructions, project structure, and development guidelines.",
                        "url": "{{docUrl}}",
                        "datePublished": "2024-01-01",
                        "dateModified": "{{DateTime.Now:yyyy-MM-dd}}",
                        "author": {
                            "@id": "{{BaseUrl}}/#organization"
                        },
                        "publisher": {
                            "@id": "{{BaseUrl}}/#organization"
                        },
                        "about": {
                            "@id": "{{BaseUrl}}/#software"
                        },
                        "isPartOf": {
                            "@id": "{{BaseUrl}}/#website"
                        },
                        "inLanguage": "en-US",
                        "proficiencyLevel": "Beginner",
                        "dependencies": [
                            ".NET 9.0 SDK",
                            "Visual Studio 2022 or VS Code",
                            "Git"
                        ],
                        "programmingLanguage": "C#",
                        "framework": "Blazor WebAssembly"
                    }
                ]
            }
            """;

        return json;
    }
}