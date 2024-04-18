using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using MyEntegrasyon.Data.Entities;
using System.Reflection.Emit;

namespace MyEntegrasyon.Data
{
    public class MyContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {

        }


        public DbSet<JsonDesen> jsonDesen { get; set; }
        public DbSet<Islem> Islem { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductVariant> ProductVariant { get; set; }

        //Detaylı arama için bunu ekledim.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// www.gencayyildiz.com/blog/entity-framework-core-lazy-loading/
            //// Microsoft.EntityFrameworkCore.Proxies
            optionsBuilder.UseLazyLoadingProxies();   ///// İlişkili tablolar kullanılırken gerekli. (incele)

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("AspNetUsers").HasKey(uc => uc.Id);
            builder.Entity<AppRole>().ToTable("AspNetRoles").HasKey(uc => uc.Id);



        }

    }
}
