using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storeme.Common
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Admin = "Admin";
        }

        public static class Controllers
        {
            public const string Home = "Home";
            public const string Cart = "Cart";
            public const string Wishlist = "Wishlist";
            public const string Products = "Products";
        }

        public static class Actions
        {
            public const string Index = "Index";
            public const string MyCart = "MyCart";
            public const string MyWishlist = "MyWishlist";
            public const string StoremeError = "StoremeError";
            public const string AllProducts = "All";            

            public const string AddToCartDetails = "AddToCartDetails";
            public const string AddToWishlistDetails = "AddToWishlistDetails";
        }

        public static class Routes
        {
            public const string Login = "/Identity/Login";
            public const string AccessDenied = "/Identity/Account/AccessDenied";
        }

        public static class ViewData
        {
            public const string Id = "Id";
            public const string Title = "Title";
            public const string Categories = "Categories";
        }

        public static class ErrorMessages
        {
            public const string CartError = "Item already exists in your cart, if not please try again. Thank you!";
            public const string WishlistError = "Item already exists in your cart, if not please try again. Thank you!";
            public const string BasicError = "Sorry, something went wrong! Please, try again.";

            public const string LoginError = "Invalid login attempt.";
        }
    }
}
