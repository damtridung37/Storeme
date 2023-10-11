using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storeme.Entities
{
    public class Wishlist : BaseEntity
    {
        public string? UserId { get; set; }

        public StoremeUser? User { get; set; }


        public List<WishlistItem>? Items { get; set; }
    }
}
