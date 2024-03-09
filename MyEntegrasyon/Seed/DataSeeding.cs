using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyEntegrasyon.Data;
using MyEntegrasyon.Data.Entities;
using System;

namespace MyEntegrasyon.Seed
{
    public class DataSeeding
    {
       


        public static async void Seed(IApplicationBuilder app)
        {
            // Inject işlemi yapmadan context sınıfını alabilmemiz gerekli. Bunun için ilk olarak Scope oluşturmamız gerekiyor.
            var scope = app.ApplicationServices.CreateScope();
            // Daha sonra bu Scope içinden Context nesnemizi alacağız.
            var context = scope.ServiceProvider.GetService<MyContext>();
            var _roleManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();
            var _userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();

            context!.Database.Migrate(); // İlk önce bekleyen Migration’larımız varsa bunları database tarafına gönderebiliriz. 
            if (context.Users.Count() == 0)
            {
                //context.Users.AddRange(
                //    new List<AppUser>() {
                //         new AppUser()
                //         {

                //             TC="23365051050",
                //             Adi="Mustafa",
                //             Soyadi="Çelenk",
                //             UserName="powerfulsoftware.mc@gmail.com",
                //             IsActive=true,
                //             Email="powerfulsoftware.mc@gmail.com",
                //             EmailConfirmed = true,
                //             Fotograf="YOK.jpeg",
                //             PasswordHash="",

                //         },
                //         new AppUser() {  
                //             TC="23365051051",
                //             Adi="Mustafa",
                //             Soyadi="Çelenk 2",
                //             UserName="mc@x.com",
                //             IsActive=true,
                //             Email="powerfulsoftware.mc@gmail.com",
                //             EmailConfirmed = true,
                //             Fotograf="YOK.jpeg",
                //              PasswordHash="",
                //         }
                //    }
                //    );

                AppUser NewUser1 = new AppUser() {
                    TC = "23365051050",
                    Adi = "Mustafa",
                    Soyadi = "Çelenk",
                    UserName = "powerfulsoftware.mc@gmail.com",
                    IsActive = true,
                    Email = "powerfulsoftware.mc@gmail.com",
                    EmailConfirmed = true,
                    Fotograf = "YOK.jpeg"
                };

                AppUser NewUser2 = new AppUser()
                {
                    TC = "23365051051",
                    Adi = "Mustafa",
                    Soyadi = "Çelenk 2",
                    UserName = "mc@x.com",
                    IsActive = true,
                    Email = "mc@x.com",
                    EmailConfirmed = true,
                    Fotograf = "YOK.jpeg"
                };


                var result_newUser1 = _userManager!.CreateAsync(NewUser1, "Pass123$123Manasiz1").Result;
                var result_newUser2 = _userManager!.CreateAsync(NewUser2, "Pass123$123Manasiz2").Result;



                bool adminRoleExists = await _roleManager!.RoleExistsAsync("Admin")!;
                if (!adminRoleExists)
                {

                    AppRole rol = new AppRole();
                    rol.Name = "Admin";
                    rol.Description = "Üst Yönetici Rolü";
                    await _roleManager.CreateAsync(rol);

                }
                bool userRoleExists = await _roleManager!.RoleExistsAsync("User")!;
                if (!userRoleExists)
                {
                    AppRole rol2 = new AppRole();
                    rol2.Name = "User";
                    rol2.Description = "Standart Kullanıcı Rolü";
                    await _roleManager.CreateAsync(rol2);
                }


                List<string> listRole1 = new List<string>();
                listRole1.Add("Admin");

                AppUser appUser1 = context.Users.Where(x => x.UserName == "powerfulsoftware.mc@gmail.com").FirstOrDefault()!;
                await _userManager!.AddToRolesAsync(appUser1, listRole1);

                // ***************************************************

                List<string> listRole2 = new List<string>();
                listRole2.Add("User");

                AppUser appUser2 = context.Users.Where(x => x.UserName == "mc@x.com").FirstOrDefault()!;
                await _userManager!.AddToRolesAsync(appUser2, listRole2);





            }





            context.SaveChanges();
        }
    }
}
