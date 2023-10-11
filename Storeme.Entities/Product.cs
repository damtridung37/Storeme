using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storeme.Entities
{
    public class Product : BaseEntity
    {
        public string? Brand { get; set; }

        public string? DeviceModel { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int Warranty { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }


        public int CategoryId { get; set; }

        public Category? Category { get; set; }


        public Item? Item { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
