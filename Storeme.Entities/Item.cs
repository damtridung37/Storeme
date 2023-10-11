using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storeme.Entities
{
    public class Item : BaseEntity
    {
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public List<CartItem>? Carts { get; set; }

        public List<WishlistItem>? Wishlists { get; set; }
    }
}
