namespace Storeme.Web.Models.Product
{
    public class ProductBindingModel
    {
        public string? Brand { get; set; }

        public string? DeviceModel { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int Warranty { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }
    }
}
