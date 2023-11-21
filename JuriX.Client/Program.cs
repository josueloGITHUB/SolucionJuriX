using JuriX.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using JuriX.Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5026") });

builder.Services.AddScoped<IDespachoService, DespachoService>();
builder.Services.AddScoped<IAbogadoService, AbogadoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICasoService, CasoService>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
