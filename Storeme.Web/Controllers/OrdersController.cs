using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storeme.Common;
using Storeme.Services.Contracts;
using Storeme.Web.Models.Cart;
using Storeme.Web.Models.Identity;
using Storeme.Web.Models.Item;
using Storeme.Web.Models.Order;

namespace Storeme.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICartService cartService;
        private readonly IMapper mapper;
        public OrdersController(IOrderService orderService, IMapper mapper, ICartService cartService)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            this.cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> My()
        {
            var orders = await this.orderService.UserOrders(this.User.Identity.Name);

            var result = mapper.Map<IEnumerable<OrderListingViewModel>>(orders);

            return View(result);
        }


        [HttpGet]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> All()
        {
            var orders = await this.orderService.GetAllOrders();

            var result = mapper.Map<IEnumerable<OrderListingViewModel>>(orders);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cart = await this.cartService.UserCart(this.User.Identity.Name);

            var user = await this.orderService.GetUser(this.User.Identity.Name);
            var userResult = mapper.Map<UserViewModel>(user);

            var items = mapper.Map<IEnumerable<CartItemViewModel>>(cart.Items);
            var result = new CartListingViewModel()
            {
                Items = items
            };

            var orders = new OrderViewModel()
            {
                User = userResult,
                Cart = result.Items
            };

            return this.View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int num)
        {
            var makeOrder = await this.orderService.CreateOrder(this.User.Identity.Name);
            return RedirectToAction(Constants.Actions.Index, Constants.Controllers.Home);
        }
    }
}
