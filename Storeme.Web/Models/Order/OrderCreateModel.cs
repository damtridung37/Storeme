using Storeme.Web.Models.Product;

namespace Storeme.Web.Models.Order
{
    public class OrderCreateModel
    {
        public int Quantity { get; set; } = 1;

        public int ProductId { get; set; }

        public ProductListingViewModel? Product { get; set; }
    }
}
