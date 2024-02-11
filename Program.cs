using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Services.Api.Control.AdminServices;
using CityFilms.Services.Api.Control.EmailServices;
using CityFilms.Services.Api.Control.HomeServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext configuration
builder.Services.AddDbContext<CityfilmsDataContext>(options =>
options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

// Services configuration
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<IHomeServices, HomeServices>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
