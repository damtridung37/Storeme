namespace Storeme.Web.Models.Product
{
    public class ProductDetailsViewModel : ProductListingViewModel
    {
        public string Description { get; set; }

        public int Warranty { get; set; }

        public int Quantity { get; set; }
    }
}
