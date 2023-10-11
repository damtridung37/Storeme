using Storeme.Web.Models.Product;

namespace Storeme.Web.Models.Order
{
    public class OrderListingViewModel
    {
        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ProductId { get; set; }

        public ProductListingViewModel? Product { get; set; }
    }
}
