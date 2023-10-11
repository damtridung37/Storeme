using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Storeme.Entities
{
    // Add profile data for application users by adding properties to the StoremeUser class
    public class StoremeUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullAddress { get; set; }

        public Cart? Cart { get; set; }

        public Wishlist? Wishlist { get; set; }

        public List<Order>? Orders { get; set; }
    }

}