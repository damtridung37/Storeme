using Microsoft.EntityFrameworkCore;
using Storeme.Data;
using Storeme.Entities;
using Storeme.Services.Contracts;

namespace Storeme.Services.Implementations
{
    public class CartService : DataService, ICartService
    {
        public CartService(StoremeDbContext context) : base(context)
        {
        }

        public async Task<bool> CreateCart(string userId)
        {
            try
            {
                var user = await this.context.Users.FindAsync(userId);
                if (user != null)
                {
                    var cart = new Cart()
                    {
                        UserId = user.Id,
                        User = user
                    };

                    user.Cart = cart;
                    this.context.Users.Update(user);
                    await this.context.Carts.AddAsync(cart);
                    this.context.SaveChanges();
                }
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<Cart> UserCart(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var cart = await this.context.Carts.Include(c => c.Items)
                .ThenInclude(i => i.Item.Product)
                .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            return cart;
        }

        public async Task<int> UserCartItemsCount(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var cart = await this.context.Carts.Include(c => c.Items).FirstOrDefaultAsync(x => x.UserId == user.Id);

            return cart.Items.Count;
        }

        public async Task<bool> AddItemToCart(int productId, string username)
        {
            try
            {
                var item = await this.context.Items.FirstOrDefaultAsync(i => i.ProductId == productId);
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

                var cart = await this.context.Carts.Include(c => c.Items)
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                var cartItem = new CartItem()
                {
                    CartId = cart.Id,
                    Cart = cart,
                    ItemId = item.Id,
                    Item = item
                };
                await this.context.CartItems.AddAsync(cartItem);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveItemFromCart(int productId, string username)
        {
            try
            {
                var item = await this.context.Items.FirstOrDefaultAsync(i => i.ProductId == productId);
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

                var cart = await this.context.Carts.Include(c => c.Items)
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                var cartItem = await this.context.CartItems.FirstOrDefaultAsync(ci => ci.ItemId == item.Id);
                this.context.CartItems.Remove(cartItem);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> IsItemInCart(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var cart = await this.context.Carts.Include(w => w.Items)
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            return cart.Items.Count;
        }

        public async Task<bool> IsItemInCart(int productId, string username)
        {
            var item = await this.context.Items.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (item == null)
            {
                return false;
            }
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var cart = await this.context.Carts.Include(w => w.Items)
                .FirstOrDefaultAsync(x => x.UserId == user.Id);
            var isInCart = await this.context.CartItems
               .FirstOrDefaultAsync(x => x.Item.ProductId == productId && x.CartId == user.Cart.Id);
            if (isInCart != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
