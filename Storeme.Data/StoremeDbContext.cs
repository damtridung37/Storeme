using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Storeme.Entities;

namespace Storeme.Data
{
    public class StoremeDbContext : IdentityDbContext<StoremeUser>
    {
        public StoremeDbContext(DbContextOptions<StoremeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product>? Products { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Item>? Items { get; set; }

        public DbSet<Cart>? Carts { get; set; }

        public DbSet<CartItem>? CartItems { get; set; }

        public DbSet<Wishlist>? Wishlists { get; set; }

        public DbSet<WishlistItem>? WishlistItems { get; set; }

        public DbSet<Order>? Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasKey(i => new { i.CartId, i.ItemId });

            modelBuilder.Entity<WishlistItem>().HasKey(i => new { i.WishlistId, i.ItemId });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Brand = "Asus",
                    DeviceModel = "ROG X",
                    Description = "Very good laptop",
                    ImageUrl = "https://s13emagst.akamaized.net/products/40929/40928236/images/res_14136f85e87f00d8c878dbd4ef30d124.png",
                    Warranty = 2,
                    Price = 1500,
                    Quantity = 5,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 2,
                    Brand = "Lenovo",
                    DeviceModel = "Legion",
                    Description = "A good one",
                    ImageUrl = "https://s13emagst.akamaized.net/products/42953/42952752/images/res_e911730d42643fa615ad91a4d8630e4b.png",
                    Warranty = 2,
                    Price = 1200,
                    Quantity = 5,
                    CategoryId = 2
                });

            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    ProductId = 1
                },
                new Item
                {
                    Id = 2,
                    ProductId = 2
                });

            modelBuilder.Entity<Category>().HasData(
               new Category
               {
                   Id = 1,
                   Title = "PC"
               },
               new Category
               {
                   Id = 2,
                   Title = "Laptop"
               },
               new Category
               {
                   Id = 3,
                   Title = "Gaming Console"
               },
               new Category
               {
                   Id = 4,
                   Title = "Mouse"
               },
               new Category
               {
                   Id = 5,
                   Title = "Keyboard"
               },
               new Category
               {
                   Id = 6,
                   Title = "Monitor"
               },
               new Category
               {
                   Id = 7,
                   Title = "Gaming Controller"
               });

            base.OnModelCreating(modelBuilder);
        }
    }
}