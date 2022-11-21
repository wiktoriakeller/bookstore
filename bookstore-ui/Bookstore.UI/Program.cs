using Bookstore.UI;
using Bookstore.UI.ApiInterfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

var apiUrl = "https://localhost:44361";
Console.WriteLine(apiUrl);

builder.Services.AddRefitClient<IPublishersApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl));

await builder.Build().RunAsync();
