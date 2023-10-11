using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Storeme.Common;
using Storeme.Entities;
using Storeme.Services.Contracts;
using Storeme.Web.Models;
using Storeme.Web.Models.Category;
using Storeme.Web.Models.Product;

namespace Storeme.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        public ProductsController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> All(
            string brand, string category, decimal priceFrom, decimal priceTo)
        {
            var products = await this.productService.GetAllProducts(brand, category, priceFrom, priceTo);
            ;
            var categories = (await this.categoryService.GetCategories())
                .Select(mapper.Map<CategoryListingViewModel>);

            ViewData[Constants.ViewData.Categories] = new SelectList(
                categories, Constants.ViewData.Id, Constants.ViewData.Title);

            var result = mapper.Map<IEnumerable<ProductListingViewModel>>(products);

            return this.View(result);
        }


		[HttpGet]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Add()
        {
            var categories = (await this.categoryService.GetCategories())
                .Select(mapper.Map<CategoryListingViewModel>);

            ViewData[Constants.ViewData.Categories] = new SelectList(
                categories, Constants.ViewData.Id, Constants.ViewData.Title);

            return this.View();

        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Add(ProductBindingModel model)
        {

            var product = mapper.Map<ProductBindingModel, Product>(model);

            if (await this.productService.CreateProduct(product))
            {
                return RedirectToAction(Constants.Actions.AllProducts, Constants.Controllers.Products);
            }

            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.BasicError });

        }

        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {

            if (await this.productService.DeleteProduct(id))
            {
                return RedirectToAction(Constants.Actions.AllProducts, Constants.Controllers.Products);
            }

            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.BasicError });

        }


        [HttpGet]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Update(int id)
        {

            var product = await this.productService.GetProduct(id);

            var result = mapper.Map<ProductUpdateModel>(product);

            var categories = (await this.categoryService.GetCategories())
                .Select(mapper.Map<CategoryListingViewModel>);

            ViewData[Constants.ViewData.Categories] = new SelectList(
                categories, Constants.ViewData.Id, Constants.ViewData.Title);

            return this.View(result);
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Update(int id, ProductUpdateModel model)
        {
            var product = mapper.Map<ProductUpdateModel, Product>(model);
            if (await this.productService.UpdateProduct(product))
            {
                return RedirectToAction(Constants.Actions.AllProducts, Constants.Controllers.Products);
            }
            return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.BasicError });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var product = await this.productService.GetProduct(id);
            if (product == null)
            {
                return RedirectToAction(Constants.Actions.StoremeError, Constants.Controllers.Home,
                new StoremeErrorViewModel() { Message = Constants.ErrorMessages.BasicError });
            }

            var result = mapper.Map<ProductDetailsViewModel>(product);

            return this.View(result);
        }
    }
}
