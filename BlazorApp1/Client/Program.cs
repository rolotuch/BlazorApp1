using BlazorApp1.Client;
using BlazorApp1.Client.Servicios;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//para mantenner las sessiones en memoria
builder.Services.AddBlazoredSessionStorage();
//para trabajar el login
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, Autenticacion>();
//preparado para inyectar en el las vistas

builder.Services.AddScoped<IServicioTienda, ServicioTienda>();

await builder.Build().RunAsync();
