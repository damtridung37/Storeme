using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storeme.Common;
using Storeme.Services.Contracts;
using Storeme.Web.Models;
using Storeme.Web.Models.Wishlist;

namespace Storeme.Web.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService wishlistService;
        private readonly IMapper mapper;
        public WishlistController(IWishlistService wishlistService, IMapper mapper)
        {
            this.wishlistService = wishlistService;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> AddToWishlist(AddToWishlistBindingModel model)
        {
            var result = await this.wishlistService.AddItemToWishlist(model.ProductId, this.User.Identity.Name);
            if (result)
            {
                return RedirectToAction(Constants.Actions.MyWishlist, Constants.Controllers.Wishlist);
            }
            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.WishlistError });
        }

        [HttpPost]
        [ActionName(Constants.Actions.AddToWishlistDetails)]
        public async Task<IActionResult> AddToWishlist(int id)
        {
            var result = await this.wishlistService.AddItemToWishlist(id, this.User.Identity.Name);
            if (result)
            {
                return RedirectToAction(Constants.Actions.MyWishlist, Constants.Controllers.Wishlist);
            }
            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.WishlistError });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(RemoveFromWishlistBindingModel model)
        {
            var result = await this.wishlistService.RemoveItemFromWishlist(model.ProductId, this.User.Identity.Name);
            if (result)
            {
                return RedirectToAction(Constants.Actions.MyWishlist, Constants.Controllers.Wishlist);
            }
            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.BasicError });
        }

        [HttpGet]
        public async Task<IActionResult> MyWishlist()
        {
            var wishlist = await this.wishlistService.UserWishlist(this.User.Identity.Name);

            var items = mapper.Map<IEnumerable<WishlistItemViewModel>>(wishlist.Items);
            var result = new WishlistListingViewModel()
            {
                Items = items
            };
            return this.View(result);
        }

        [HttpGet]
        public async Task<int> WishlistCount()
        {
            var itemsCount = await this.wishlistService.WishlistItemsCount(this.User.Identity.Name);


            return itemsCount;
        }
    }
}
