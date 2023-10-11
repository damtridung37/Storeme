using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storeme.Common;
using Storeme.Services.Contracts;
using Storeme.Web.Models;
using Storeme.Web.Models.Cart;
using Storeme.Web.Models.Item;

namespace Storeme.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IMapper mapper;
        public CartController(ICartService cartService, IMapper mapper)
        {
            this.cartService = cartService;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartBindingModel model)
        {
            var result = await this.cartService.AddItemToCart(model.ProductId, this.User.Identity.Name);
            if (result)
            {
                return RedirectToAction(Constants.Actions.MyCart, Constants.Controllers.Cart);
            }
            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.CartError });
        }

        [HttpPost]
        [ActionName(Constants.Actions.AddToCartDetails)]
        public async Task<IActionResult> AddToCart(int id)
        {
            var result = await this.cartService.AddItemToCart(id, this.User.Identity.Name);
            if (result)
            {
                return RedirectToAction(Constants.Actions.MyCart, Constants.Controllers.Cart);
            }
            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.CartError });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(RemoveFromCartBindingModel model)
        {
            var result = await this.cartService.RemoveItemFromCart(model.ProductId, this.User.Identity.Name);
            if (result)
            {
                return RedirectToAction(Constants.Actions.MyCart, Constants.Controllers.Cart);
            }
            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.BasicError });
        }

        [HttpGet]
        public async Task<IActionResult> MyCart()
        {
            var cart = await this.cartService.UserCart(this.User.Identity.Name);

            var items = mapper.Map<IEnumerable<CartItemViewModel>>(cart.Items);
            var result = new CartListingViewModel()
            {
                Items = items
            };
            return this.View(result);
        }

        [HttpGet]
        public async Task<int> CartCount()
        {
            return await this.cartService.UserCartItemsCount(this.User.Identity.Name);

        }

        [HttpPost]
        public async Task<bool> CheckProduct([FromBody] BaseBindingModel model)
        {
            return await this.cartService.IsItemInCart(model.ProductId, this.User.Identity.Name);

        }
    }
}
