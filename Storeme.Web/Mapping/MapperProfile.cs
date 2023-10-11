using AutoMapper;
using Storeme.Entities;
using Storeme.Web.Models.Cart;
using Storeme.Web.Models.Category;
using Storeme.Web.Models.Identity;
using Storeme.Web.Models.Item;
using Storeme.Web.Models.Order;
using Storeme.Web.Models.Product;
using Storeme.Web.Models.Wishlist;

namespace Storeme.Web.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductListingViewModel>().ReverseMap();
            CreateMap<Product, ProductUpdateModel>().ReverseMap();
            CreateMap<Product, ProductBindingModel>().ReverseMap();
            //
            CreateMap<Category, CategoryListingViewModel>().ReverseMap();
            CreateMap<Item, ItemListingViewModel>().ReverseMap();
            CreateMap<Product, ProductDetailsViewModel>().ReverseMap();
            //cart
            CreateMap<Cart, CartListingViewModel>().ReverseMap();
            CreateMap<CartItem, ItemListingViewModel>().ReverseMap();
            CreateMap<CartItem, CartItemViewModel>().ReverseMap();
            //wishlist
            CreateMap<Wishlist, WishlistListingViewModel>().ReverseMap();
            CreateMap<WishlistItem, ItemListingViewModel>().ReverseMap();
            CreateMap<WishlistItem, WishlistItemViewModel>().ReverseMap();
            //order
            CreateMap<Order, OrderListingViewModel>().ReverseMap();
            CreateMap<Order, OrderCreateModel>().ReverseMap();
            //user
            CreateMap<StoremeUser, UserViewModel>().ReverseMap();

        }
    }
}
