using Storeme.Entities;

namespace Storeme.Services.Contracts
{
    public interface ICartService
    {
        Task<bool> CreateCart(string userId);

        Task<Cart> UserCart(string username);

        Task<int> UserCartItemsCount(string username);


        Task<bool> AddItemToCart(int productId, string username);

        Task<bool> RemoveItemFromCart(int productId, string username);

        Task<int> IsItemInCart(string username);

        Task<bool> IsItemInCart(int productId, string username);
    }
}
