using Storeme.Web.Models.Product;

namespace Storeme.Web.Models.Item
{
    public class ItemListingViewModel
    {
        public int ProductId { get; set; }

        public ProductListingViewModel? Product { get; set; }

    }
}
