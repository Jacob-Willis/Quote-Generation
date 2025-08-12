using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QuoteGeneration;
using QuoteGeneration.Controllors;
using QuoteGeneration.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#if DEBUG
    var apiBase = "https://localhost:7119/"; // <-- confirm your API port
#else
    var apiBase = "https://YOUR-PROD-API/";  // e.g., https://quoteapi.yourdomain.com/
#endif

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });
builder.Services.AddScoped<Query>();
builder.Services.AddSingleton<SavedQuotesService>();

await builder.Build().RunAsync();
