using Storeme.Web.Models.Cart;
using Storeme.Web.Models.Identity;
using Storeme.Web.Models.Item;
using Storeme.Web.Models.Product;

namespace Storeme.Web.Models.Order
{
    public class OrderViewModel
    {
        public UserViewModel? User { get; set; }

        public IEnumerable<CartItemViewModel>? Cart { get; set; }
    }
}
