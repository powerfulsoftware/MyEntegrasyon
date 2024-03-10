using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyEntegrasyon.Data;
using MyEntegrasyon.Data.Entities;
using MyEntegrasyon.Seed;
using MyEntegrasyon.Service;
using MyEntegrasyon.Services;


using Serilog;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Debug(new RenderedCompactJsonFormatter() )
    .WriteTo.File("log.txt", rollingInterval:RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting web application MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");

    var builder = WebApplication.CreateBuilder(args);


    builder.Host.UseSerilog(); // <-- Add this line

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddScoped<IMathService, MatchService>();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddIdentity<AppUser, AppRole>(opt =>
    {
        opt.Password.RequireDigit = false;              //// Parolada 0-9 aras�nda bir say� gerekir.
        opt.Password.RequiredLength = 5;                //// Bir parolan�n olmas� gereken minimum uzunlu�u al�r veya ayarlar. Varsay�lan olarak 6 ' ya d�ner.
        opt.Password.RequiredUniqueChars = 1;           //// Bir parolan�n i�ermesi gereken en az benzersiz karakter say�s�n� al�r veya ayarlar. Varsay�lan de�er 1 ' dir.
        opt.Password.RequireLowercase = true;           //// Parolalar�n k���k harf ASCII karakteri i�ermesi gerekti�ini belirten bir bayrak al�r veya ayarlar. Varsay�lan olarak true de�erini al�r.
        opt.Password.RequireNonAlphanumeric = true;     //// Parolalar�n alfasay�sal olmayan bir karakter i�ermesi gerekti�ini belirten bir bayrak al�r veya ayarlar. Varsay�lan olarak true de�erini al�r.
        opt.Password.RequireUppercase = false;          //// Parolalar�n b�y�k harfli bir ASCII karakteri i�ermesi gerekti�ini belirten bir bayrak al�r veya ayarlar. Varsay�lan olarak true de�erini al�r.
        opt.User.RequireUniqueEmail = false;            //// Uygulaman�n kullan�c�lar� i�in benzersiz e-postalar gerektirip gerektirmedi�ini belirten bir bayrak al�r veya ayarlar. Varsay�lan de�er false.
                                                        // opt.SignIn.RequireConfirmedAccount = false;      //// Oturum a�mak i�in confirmed I User Confirmation<User> hesab�n�n gerekli olup olmad���n� belirten bir bayrak al�r veya ayarlar. Varsay�lan de�er false.
                                                        // opt.SignIn.RequireConfirmedEmail = false;        //// Oturum a�mak i�in onaylanm�� bir e-posta adresinin gerekli olup olmad���n� belirten bir bayrak al�r veya ayarlar. Varsay�lan de�er false.
                                                        // opt.SignIn.RequireConfirmedPhoneNumber = false;  //// Oturum a�mak i�in onaylanm�� bir telefon numaras�n�n gerekli olup olmad���n� belirten bir bayrak al�r veya ayarlar. Varsay�lan de�er false.

    })
       .AddDefaultTokenProviders()
       .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>()
      //.AddDefaultUI()  // Kullan�c� arabirimi (UI) oturum a�ma i�levini destekleyen bir API'dir.  / d�� oturum a�ma sa�lay�c�s� kullanabilir. / Facebook, Google, Microsoft Hesab� ve Twitter
      .AddEntityFrameworkStores<MyContext>();


    builder.Services.ConfigureApplicationCookie(opts =>
    {
        opts.LoginPath = new PathString("/Home/Login");
        opts.LogoutPath = new PathString("/Home/Logout");
        opts.Cookie = new CookieBuilder
        {
            // Name = "AspNetCoreIdentityExampleCookie", //Olu�turulacak Cookie'yi isimlendiriyoruz.
            Name = "MyEntegrasyonCookies", //Olu�turulacak Cookie'yi isimlendiriyoruz.
            HttpOnly = false, //K�t� niyetli insanlar�n client-side taraf�ndan Cookie'ye eri�mesini engelliyoruz.
                              // Expiration = TimeSpan.FromMinutes(2), //Olu�turulacak Cookie'nin vadesini belirliyoruz.
            SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin g�nderilmemesini belirtiyoruz.
            SecurePolicy = CookieSecurePolicy.Always //HTTPS �zerinden eri�ilebilir yap�yoruz.
        };
        opts.SlidingExpiration = true; //Expiration s�resinin yar�s� kadar s�re zarf�nda istekte bulunulursa e�er geri kalan yar�s�n� tekrar s�f�rlayarak ilk ayarlanan s�reyi tazeleyecektir.
        opts.ExpireTimeSpan = TimeSpan.FromMinutes(2); //CookieBuilder nesnesinde tan�mlanan Expiration de�erinin varsay�lan de�erlerle ezilme ihtimaline kar��n tekrardan Cookie vadesi burada da belirtiliyor.
    });




    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    DataSeeding.Seed(app);

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex)
{

    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}




