using GayathrisDeliciousPie.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));


builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddJsonOptions( options =>{ options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
builder.Services.AddDbContext<GayathrisPieShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:GayathrisPieShopDbContextConnection"]);
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
