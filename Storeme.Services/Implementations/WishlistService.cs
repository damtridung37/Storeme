using Microsoft.EntityFrameworkCore;
using Storeme.Data;
using Storeme.Entities;
using Storeme.Services.Contracts;

namespace Storeme.Services.Implementations
{
    public class WishlistService : DataService, IWishlistService
    {
        public WishlistService(StoremeDbContext context) : base(context)
        {
        }
        public async Task<bool> CreateWishlist(string userId)
        {
            try
            {
                var user = await this.context.Users.FindAsync(userId);
                if (user != null)
                {
                    var wishlist = new Wishlist()
                    {
                        UserId = user.Id,
                        User = user
                    };

                    user.Wishlist = wishlist;
                    this.context.Users.Update(user);
                    await this.context.Wishlists.AddAsync(wishlist);
                    this.context.SaveChanges();
                }
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<int> UserWishlistItemsCount(string userId)
        {
            var user = await this.context.Users.FindAsync(userId);
            var wishlist = await this.context.Wishlists.Include(c => c.Items).FirstOrDefaultAsync(x => x.UserId == userId);

            return wishlist.Items.Count;
        }

        public async Task<Wishlist> UserWishlist(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x=>x.UserName == username);
            var wishlist = await this.context.Wishlists.Include(w => w.Items)
                .ThenInclude(i => i.Item.Product)
                .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            return wishlist;
        }

        public async Task<bool> AddItemToWishlist(int productId, string username)
        {
            try
            {
                var item = await this.context.Items.FirstOrDefaultAsync(i => i.ProductId == productId);
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

                var wishlist = await this.context.Wishlists.Include(w => w.Items)
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                var wishlistItem = new WishlistItem()
                {
                    WishlistId = wishlist.Id,
                    Wishlist = wishlist,
                    ItemId = item.Id,
                    Item = item
                };
                await this.context.WishlistItems.AddAsync(wishlistItem);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveItemFromWishlist(int productId, string username)
        {
            try
            {
                var item = await this.context.Items.FirstOrDefaultAsync(i => i.ProductId == productId);
                var user = await this.context.Users.FirstOrDefaultAsync(x=>x.UserName == username);

                var wishlist = await this.context.Wishlists.Include(w => w.Items)
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                var wishlistItem = await this.context.WishlistItems.FirstOrDefaultAsync(ci => ci.ItemId == item.Id);
                this.context.WishlistItems.Remove(wishlistItem);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> WishlistItemsCount(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x=>x.UserName == username);

            var wishlist = await this.context.Wishlists.Include(w => w.Items)
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            return wishlist.Items.Count;
        }
    }
}
