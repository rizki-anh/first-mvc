using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using user.Extensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
    options.Cookie.Name = "YourAppCookie"; // Nama cookie
    options.LoginPath = "/Login/login";  // Path untuk redirect jika belum login
    options.AccessDeniedPath = "/Account/AccessDenied"; // Path jika akses ditolak
    options.ExpireTimeSpan = TimeSpan.FromDays(30); // Durasi cookie berlaku
    options.Cookie.HttpOnly = true; // Cookie hanya bisa diakses via HTTP (aman dari XSS)
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Hanya dikirim via HTTPS
    options.Cookie.SameSite = SameSiteMode.Strict; // Proteksi CSRF
    options.SlidingExpiration = true; // Perpanjang durasi cookie otomatis
    });

builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
});


builder.Services.AddControllersWithViews();
builder.Services.AddMyDependencies(builder.Configuration);
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=login}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "register",
    pattern: "Register",
    defaults: new { controller = "Register", action = "Register" });
app.Run();
