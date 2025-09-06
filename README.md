# TextHub

A modern, responsive web application built with Blazor WebAssembly that provides quick and simple text utilities. TextHub offers a comprehensive suite of text manipulation tools including case conversion, word counting, and text formatting.


## 🚀 Live Demo

Visit the live application at: [https://codefrydev.in/TextHub/](https://codefrydev.in/TextHub/)

## 🛠️ Technology Stack

- **Frontend**: Blazor WebAssembly (.NET 9.0)
- **Styling**: Tailwind CSS
- **Icons**: Lucide Icons
- **Deployment**: GitHub Pages
- **CI/CD**: GitHub Actions

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code with C# extension

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/TextHub.git
   cd TextHub
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run --project TextHub/TextHub.csproj
   ```

4. **Open your browser**
   Navigate to `https://localhost:5001` (or the URL shown in the terminal)

## 🚀 Deployment

The application is automatically deployed to GitHub Pages using GitHub Actions. The deployment workflow:

1. Builds the Blazor WebAssembly application
2. Changes the base href from `/` to `/TextHub/` for GitHub Pages
3. Creates a 404.html file for SPA fallback
4. Deploys to the `gh-pages` branch

### Manual Deployment

To deploy manually:

```bash
dotnet publish TextHub/TextHub.csproj -c Release -o release
# The release folder contains the deployable files
```

## 🐛 Common Issues & Solutions

### Issue: 404 Errors on Direct URL Access

**Problem**: When you use `/uppercase` for navigation not work as expected

Example : 
=> website is hoisted on `codefrydev.in/TetxHub`

=> With `/uppercase` it will got to `codefrydev.in/uppercase` which causes issue
=> With `oppercase` it will go to 'codefrydev.in/TextHub/uppercase`

**Solution**: 
1. **Fixed Navigation Links**: Removed leading slashes from all tool links in `ToolDataService.cs`
   ```csharp
   // Before (causing 404s)
   new Tool("Uppercase Converter", "Convert any text to UPPERCASE instantly", "/uppercase", "...")
   
   // After (working correctly)
   new Tool("Uppercase Converter", "Convert any text to UPPERCASE instantly", "uppercase", "...")
   ```

2. **Fixed Navigation Bar**: Updated all navigation links to use relative paths
   ```html
   <!-- Before -->
   <a href="/">Text Hub</a>
   <a href="/uppercase">Uppercase</a>
   
   <!-- After -->
   <a href="">Text Hub</a>
   <a href="uppercase">Uppercase</a>
   ```

3. **Deployment Configuration**: The deployment workflow automatically:
   - Changes base href to `/TextHub/` for GitHub Pages
   - Creates a 404.html file for SPA fallback
   - Ensures relative links resolve correctly

### Issue: Links Not Working After Deployment

**Problem**: Tool cards and navigation links don't work after deploying to GitHub Pages.

**Solution**: Ensure all links use relative paths instead of absolute paths. The base href configuration in the deployment handles the rest.

## 🎨 Customization

### Adding New Tools

1. **Create the Razor component** in the appropriate `Features/` folder
2. **Add the route** using `@page "/tool-name"`
3. **Update ToolDataService.cs** to include the new tool
4. **Add the tool to the appropriate section** (TextCase, TextAnalysis, or TextFormatting)

Example:
```csharp
// In ToolDataService.cs
new Tool("New Tool", "Description of the new tool", "new-tool", "<svg>...</svg>")
```

### Styling

The application uses Tailwind CSS for styling. Key customization points:

- **Colors**: Defined in `wwwroot/index.html` in the Tailwind config
- **Components**: Styled using Tailwind classes
- **Animations**: Custom animations defined in the CSS

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [Blazor](https://blazor.net/) for the web framework
- [Tailwind CSS](https://tailwindcss.com/) for styling
- [Lucide Icons](https://lucide.dev/) for beautiful icons
- [GitHub Pages](https://pages.github.com/) for hosting

## 📞 Support

If you encounter any issues or have questions, please:

1. Check the [Common Issues & Solutions](#-common-issues--solutions) section
2. Search existing [GitHub Issues](https://github.com/codefrydev/TextHub/issues)
3. Create a new issue if your problem isn't already documented

---

**Made with ❤️ using Blazor WebAssembly**
