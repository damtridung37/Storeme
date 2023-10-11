using Storeme.Entities;

namespace Storeme.Services.Contracts
{
    public interface IWishlistService
    {
        Task<bool> CreateWishlist(string userId);

        Task<Wishlist> UserWishlist(string username);

        Task<int> UserWishlistItemsCount(string userId);

        Task<bool> AddItemToWishlist(int productId, string username);

        Task<bool> RemoveItemFromWishlist(int productId, string username);

        Task<int> WishlistItemsCount(string username);
    }
}
