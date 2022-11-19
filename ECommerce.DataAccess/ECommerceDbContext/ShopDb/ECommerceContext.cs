using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete.Enum;
using ECommerce.Entities.Entities.UserEntities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.ECommerceDbContext.ShopDb
{
    public class ECommerceContext : IdentityDbContext<User,Role, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BIYIKLI\\BIYIKLI;Database=ECommerceShopDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");


            modelBuilder
                .Entity<Product>()
                .Property(x => x.Gender)
                .HasConversion(
                    value => value.ToString(),
                    value => (Genders)Enum.Parse(typeof(Genders), value));

            modelBuilder
                .Entity<Product>()
                .Property(x => x.BodyPart)
                .HasConversion(
                    value => value.ToString(),
                    value => (BodyParts)Enum.Parse(typeof(BodyParts), value));
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
