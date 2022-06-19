using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("sqlConnection")));
builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: null,
    pattern: "{category}/Page{page:int}",
    defaults: new { controller = "Product", action = "List"});

app.MapControllerRoute(
    name: null,
    pattern: "Page{page:int}",
    defaults: new { controller = "Product", action = "List", page = 1});

app.MapControllerRoute(
    name: null,
    pattern: "category",
    defaults: new { controller = "Product", action = "List", page = 1});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=List}/{id?}");
app.Run();

