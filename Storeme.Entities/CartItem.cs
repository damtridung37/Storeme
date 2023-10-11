using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storeme.Entities
{
    public class CartItem 
    {
        public int? CartId { get; set; }

        public Cart? Cart { get; set; }


        public int? ItemId { get; set; }

        public Item? Item { get; set; }
    }
}
