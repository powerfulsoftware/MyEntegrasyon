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
        opt.Password.RequireDigit = false;              //// Parolada 0-9 arasýnda bir sayý gerekir.
        opt.Password.RequiredLength = 5;                //// Bir parolanýn olmasý gereken minimum uzunluðu alýr veya ayarlar. Varsayýlan olarak 6 ' ya döner.
        opt.Password.RequiredUniqueChars = 1;           //// Bir parolanýn içermesi gereken en az benzersiz karakter sayýsýný alýr veya ayarlar. Varsayýlan deðer 1 ' dir.
        opt.Password.RequireLowercase = true;           //// Parolalarýn küçük harf ASCII karakteri içermesi gerektiðini belirten bir bayrak alýr veya ayarlar. Varsayýlan olarak true deðerini alýr.
        opt.Password.RequireNonAlphanumeric = true;     //// Parolalarýn alfasayýsal olmayan bir karakter içermesi gerektiðini belirten bir bayrak alýr veya ayarlar. Varsayýlan olarak true deðerini alýr.
        opt.Password.RequireUppercase = false;          //// Parolalarýn büyük harfli bir ASCII karakteri içermesi gerektiðini belirten bir bayrak alýr veya ayarlar. Varsayýlan olarak true deðerini alýr.
        opt.User.RequireUniqueEmail = false;            //// Uygulamanýn kullanýcýlarý için benzersiz e-postalar gerektirip gerektirmediðini belirten bir bayrak alýr veya ayarlar. Varsayýlan deðer false.
                                                        // opt.SignIn.RequireConfirmedAccount = false;      //// Oturum açmak için confirmed I User Confirmation<User> hesabýnýn gerekli olup olmadýðýný belirten bir bayrak alýr veya ayarlar. Varsayýlan deðer false.
                                                        // opt.SignIn.RequireConfirmedEmail = false;        //// Oturum açmak için onaylanmýþ bir e-posta adresinin gerekli olup olmadýðýný belirten bir bayrak alýr veya ayarlar. Varsayýlan deðer false.
                                                        // opt.SignIn.RequireConfirmedPhoneNumber = false;  //// Oturum açmak için onaylanmýþ bir telefon numarasýnýn gerekli olup olmadýðýný belirten bir bayrak alýr veya ayarlar. Varsayýlan deðer false.

    })
       .AddDefaultTokenProviders()
       .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>()
      //.AddDefaultUI()  // Kullanýcý arabirimi (UI) oturum açma iþlevini destekleyen bir API'dir.  / dýþ oturum açma saðlayýcýsý kullanabilir. / Facebook, Google, Microsoft Hesabý ve Twitter
      .AddEntityFrameworkStores<MyContext>();


    builder.Services.ConfigureApplicationCookie(opts =>
    {
        opts.LoginPath = new PathString("/Home/Login");
        opts.LogoutPath = new PathString("/Home/Logout");
        opts.Cookie = new CookieBuilder
        {
            // Name = "AspNetCoreIdentityExampleCookie", //Oluþturulacak Cookie'yi isimlendiriyoruz.
            Name = "MyEntegrasyonCookies", //Oluþturulacak Cookie'yi isimlendiriyoruz.
            HttpOnly = false, //Kötü niyetli insanlarýn client-side tarafýndan Cookie'ye eriþmesini engelliyoruz.
                              // Expiration = TimeSpan.FromMinutes(2), //Oluþturulacak Cookie'nin vadesini belirliyoruz.
            SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin gönderilmemesini belirtiyoruz.
            SecurePolicy = CookieSecurePolicy.Always //HTTPS üzerinden eriþilebilir yapýyoruz.
        };
        opts.SlidingExpiration = true; //Expiration süresinin yarýsý kadar süre zarfýnda istekte bulunulursa eðer geri kalan yarýsýný tekrar sýfýrlayarak ilk ayarlanan süreyi tazeleyecektir.
        opts.ExpireTimeSpan = TimeSpan.FromMinutes(2); //CookieBuilder nesnesinde tanýmlanan Expiration deðerinin varsayýlan deðerlerle ezilme ihtimaline karþýn tekrardan Cookie vadesi burada da belirtiliyor.
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




