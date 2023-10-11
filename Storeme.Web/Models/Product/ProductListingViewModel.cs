namespace Storeme.Web.Models.Product
{
    public class ProductListingViewModel
    {
        public int Id { get; set; }
        public string? Brand { get; set; }

        public string? DeviceModel { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string? CategoryTitle { get; set; }
    }
}
