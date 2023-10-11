namespace Storeme.Web.Models.Item
{
    public class CartItemViewModel
    {
        public int ItemId { get; set; }

        public ItemListingViewModel? Item { get; set; }
    }
}
