using AkademiQMongoDb.Services.AboutServices;
using AkademiQMongoDb.Services.AdminServices;
using AkademiQMongoDb.Services.BannerServices;
using AkademiQMongoDb.Services.CategoryServices;
using AkademiQMongoDb.Services.ChefServices;
using AkademiQMongoDb.Services.ProductServices;
using AkademiQMongoDb.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Ayarları
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

// 2. Servis Bağlantıları (Dependency Injection)
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IChefService, ChefService>();
builder.Services.AddScoped<IAboutService, AboutService>();

// 3. MVC ve Global Güvenlik Ayarları (Her sayfa için Login zorunluluğu)
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

// 4. SEPET İÇİN EKLENEN KISIM: Hafıza (Cache) ve Session Ayarları
builder.Services.AddDistributedMemoryCache(); // Session'ın kullanacağı RAM alanını açar
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sepet süresi 30 dk
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 5. Kimlik Doğrulama (Cookie Auth) Ayarları
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Login/Signin";
        config.LogoutPath = "/Login/Logout";
        config.Cookie.Name = "FooduAppCookie";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        config.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession(); // Sepet hafızasını devreye alıyoruz

// Rotalar
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();