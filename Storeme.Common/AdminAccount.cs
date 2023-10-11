using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Storeme.Data;
using Storeme.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storeme.Common
{
    public class AdminAccount
    {
        public static void SetupAdminAccount(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<StoremeDbContext>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<StoremeUser>>();

                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                }

                if (userManager.FindByNameAsync("admin").Result == null)
                {
                    var adminUser = new StoremeUser();
                    adminUser.UserName = "admin";
                    adminUser.Email = "admin@admin.com";
                    adminUser.FirstName = "Admin";
                    adminUser.LastName = "Ivanov";
                    adminUser.PhoneNumber = "359123456789";
                    adminUser.FullAddress = "Sofia, Bulgaria";

                    string adminPassword = "123456";

                    IdentityResult result = userManager.CreateAsync(adminUser, adminPassword).Result;

                    var adminCart = new Cart();
                    adminCart.UserId = adminUser.Id;
                    adminCart.User = adminUser;

                    var adminWishlist = new Wishlist();
                    adminWishlist.UserId = adminUser.Id;
                    adminWishlist.User = adminUser;

                    adminUser.Cart = adminCart;
                    adminUser.Wishlist = adminWishlist;

                    dbContext.Carts?.Add(adminCart);
                    dbContext.Wishlists?.Add(adminWishlist);
                    dbContext.SaveChanges();

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }
            }
        }
    }
}
