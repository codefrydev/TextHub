using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TextHub;
using TextHub.Services.Data;
using TextHub.Services.Converters;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ToolDataService>();
builder.Services.AddScoped<JsonLdService>();
builder.Services.AddScoped<TextCaseConverterService>();

await builder.Build().RunAsync();