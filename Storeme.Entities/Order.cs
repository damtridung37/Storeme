using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storeme.Entities
{
    public class Order : BaseEntity
    {
        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsFinished { get; set; }

        public string? UserId { get; set; }

        public StoremeUser? User { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
