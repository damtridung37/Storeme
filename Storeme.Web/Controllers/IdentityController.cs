using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Storeme.Common;
using Storeme.Entities;
using Storeme.Services.Contracts;
using Storeme.Web.Models.Identity;

namespace Storeme.Web.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<StoremeUser> _signInManager;
        private readonly UserManager<StoremeUser> _userManager;
        private readonly IUserStore<StoremeUser> _userStore;
        private readonly IUserEmailStore<StoremeUser> _emailStore;
        private readonly ICartService cartService;
        private readonly IWishlistService wishlistService;

        public IdentityController(
            UserManager<StoremeUser> userManager,
            IUserStore<StoremeUser> userStore,
            SignInManager<StoremeUser> signInManager,
            IWishlistService wishlistService,
            ICartService cartService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            this.wishlistService = wishlistService;
            this.cartService = cartService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel input)
        {

            var user = CreateUser();
            user.FirstName = input.FirstName;
            user.LastName = input.Lastname;
            user.PhoneNumber = input.Phone;
            user.FullAddress = input.Address;

            await _userStore.SetUserNameAsync(user, input.Username, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                var cart = await this.cartService.CreateCart(user.Id);
                var wishlist = await this.wishlistService.CreateWishlist(user.Id);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(Constants.Actions.Index, Constants.Controllers.Home);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            // If we got this far, something failed, redisplay form
            return this.View();
        }

        [HttpGet]
        [Route(Constants.Routes.Login)]
        //[Route("/Identity/Account/Login")]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction(Constants.Actions.Index, Constants.Controllers.Home);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Constants.ErrorMessages.LoginError);
                    return this.View();
                }
            }
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(Constants.Actions.Index, Constants.Controllers.Home);

        }
        private StoremeUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<StoremeUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(StoremeUser)}'. " +
                    $"Ensure that '{nameof(StoremeUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<StoremeUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<StoremeUser>)_userStore;
        }

        [HttpGet]
        [Route(Constants.Routes.AccessDenied)]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
