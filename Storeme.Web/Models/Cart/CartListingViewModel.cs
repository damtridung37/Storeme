using Storeme.Web.Models.Item;
using Storeme.Web.Models.Product;

namespace Storeme.Web.Models.Cart
{
    public class CartListingViewModel
    {
        public IEnumerable<CartItemViewModel>? Items { get; set; }
    }
}
