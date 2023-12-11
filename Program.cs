using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OttoFlights.Data;
using OttoFlights.Hubs;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VolContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VolContext") ?? throw new InvalidOperationException("Connection string 'VolContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSession(options =>
{ 
 options.IdleTimeout = TimeSpan.FromMinutes(30); 
 options.Cookie.Name = ".AspNetCore.Session"; 
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
app.MapHub<VolHub>("/VolHub");

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSession();
app.Run();