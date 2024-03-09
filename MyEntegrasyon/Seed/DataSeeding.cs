using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyEntegrasyon.Data;
using MyEntegrasyon.Data.Entities;
using System;

namespace MyEntegrasyon.Seed
{
    public class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            // Inject işlemi yapmadan context sınıfını alabilmemiz gerekli. Bunun için ilk olarak Scope oluşturmamız gerekiyor.
            var scope = app.ApplicationServices.CreateScope();
            // Daha sonra bu Scope içinden Context nesnemizi alacağız.
            var context = scope.ServiceProvider.GetService<MyContext>();
            context!.Database.Migrate(); // İlk önce bekleyen Migration’larımız varsa bunları database tarafına gönderebiliriz. 
            if (context.Users.Count() == 0)
            {
                context.Users.AddRange(
                    new List<AppUser>() {
                         new AppUser()
                         {
                             Id= 1,
                             TC="23365051050",
                             Adi="Mustafa",
                             Soyadi="Çelenk",
                             UserName="powerfulsoftware.mc@gmail.com",
                             IsActive=true,
                             Email="powerfulsoftware.mc@gmail.com",
                             EmailConfirmed = true,
                             Fotograf="YOK.jpeg"
                         },
                         new AppUser() {  Id= 1,
                             TC="23365051051",
                             Adi="Mustafa",
                             Soyadi="Çelenk 2",
                             UserName="mc@x.com",
                             IsActive=true,
                             Email="powerfulsoftware.mc@gmail.com",
                             EmailConfirmed = true,
                             Fotograf="YOK.jpeg"
                         }






                    }
                    );
            }
            context.SaveChanges();
        }
    }
}
