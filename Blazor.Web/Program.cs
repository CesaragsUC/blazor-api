using Blazor.Web;
using Blazor.Web.AppStates;
using Blazor.Web.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUrl = "https://localhost:7200";

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(baseUrl) 
});

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICarrinhoCompraService, CarrinhoCompraService>();
builder.Services.AddScoped<AppCarrinhoState>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IGerenciaCarrinhoItensLocalStorageService,
    GerenciaCarrinhoItensLocalStorageService>();

builder.Services.AddScoped<IGerenciaProdutosLocalStorageService,
    GerenciaProdutosLocalStorageService>();

await builder.Build().RunAsync();
