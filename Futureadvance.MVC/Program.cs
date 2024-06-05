using Booky.MVC.Services;
using Futureadvance.MVC.Services.Iservices;
using Futureadvance.Repostiory.Data;
using Futureadvance.Repostiory.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Futureadvance.Utility;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<FutureadvanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
{
    // Configure password requirements
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;

    // Configure lockout settings
    options.Lockout.MaxFailedAccessAttempts = 5;

    // Configure user settings
    options.User.RequireUniqueEmail = true;

    // Other configuration options...
}).AddEntityFrameworkStores<FutureadvanceDbContext>().AddDefaultTokenProviders();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyCustomCookieName"; // Set custom cookie name

        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
        options.LoginPath = "/User/Security/LoginForm";
    });

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IUserservice, UserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseAuthentication(); // Ensure this is before UseAuthorization
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
