using BlueStarMVC.Models;
using BlueStarMVC.Pages.Server;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BluestarContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BluestarDbContext")));
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VlhhQlVHfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9SdkNjW35ec3FcQ2dV");

// Add this block to configure services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(builder => {
    builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

}); // Enable CORS

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    endpoints.MapFallbackToFile("index.html");
});


app.Run();
