using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storeme.Entities
{
    public class Cart : BaseEntity
    {
        public string? UserId { get; set; }

        public StoremeUser? User { get; set; }


        public List<CartItem>? Items { get; set; }
    }
}
