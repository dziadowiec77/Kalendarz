using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Kalendarz.Areas.Identity.Data;
using Kalendarz.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("KalendarzDBContextConnection") ?? throw new InvalidOperationException("Connection string 'KalendarzDBContextConnection' not found.");

builder.Services.AddDbContext<KalendarzDBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<KalendarzUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<KalendarzDBContext>();
builder.Services.AddDbContext<KalendarzDBContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
