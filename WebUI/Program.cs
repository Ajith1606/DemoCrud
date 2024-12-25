using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Application.DependencyInjection;
using WebUI;
using NetcodeHub.Packages.Components.Toast;
using Blazored.Toast;
using Blazored.Toast.Services;
using NetcodeHub.Packages.Components;
using NetcodeHub.Packages.Components.DataGrid;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddApplicationService();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<ToastService>();
builder.Services.AddVirtualizationService();
await builder.Build().RunAsync();
