using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GayathrisDeliciousPie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));

//if (builder.Environment.IsProduction()) {
//    var keyVaultURL = builder.Configuration.GetSection("KeyVault:url");
//    var keyClientId = builder.Configuration.GetSection("KeyVault:clientId");
//    var keyClientSecret = builder.Configuration.GetSection("KeyVault:clientSecret");
//    var keyDirectoryId = builder.Configuration.GetSection("KeyVault:directoryId");

//    var credential = new ClientSecretCredential(keyDirectoryId.Value!.ToString(), keyClientId.Value!.ToString(),keyClientSecret.Value!.ToString());
//    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), keyClientId.Value!.ToString(), keyClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());
//    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);
    
//    builder.Services.AddDbContext<GayathrisPieShopDbContext>(options =>
//    {
//        //options.UseSqlServer(builder.Configuration["ConnectionStrings:GayathrisPieShopDbContextConnection1"]);
//        options.UseSqlServer(client.GetSecret("ProdConnectionDB").Value.Value.ToString());
//    });
//}


builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddJsonOptions( options =>{ options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
builder.Services.AddDbContext<GayathrisPieShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:GayathrisPieShopDbContextConnection1"]);
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

if(app.Environment.IsDevelopment())
app.UseDeveloperExceptionPage();

app.MapDefaultControllerRoute();

app.UseAntiforgery();

app.MapRazorPages();

DbInitializer.Seed(app);



app.Run();
