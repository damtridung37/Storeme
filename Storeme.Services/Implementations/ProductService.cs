using Microsoft.EntityFrameworkCore;
using Storeme.Data;
using Storeme.Entities;
using Storeme.Services.Contracts;

namespace Storeme.Services.Implementations
{
    public class ProductService : DataService, IProductService
    {
        public ProductService(StoremeDbContext context) : base(context) { }

        public async Task<bool> CreateProduct(Product product)
        {
            try
            {
                var item = new Item()
                {
                    ProductId = product.Id,
                    Product = product
                };

                await this.context.Items.AddAsync(item);
                await this.context.Products.AddAsync(product);
                this.context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id);
                this.context.Products.Remove(product);
                this.context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProduct(Product model)
        {
            try
            {
                var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == model.Id);
                product.Brand = model.Brand;
                product.DeviceModel = model.DeviceModel;
                product.ImageUrl = model.ImageUrl;
                product.Warranty = model.Warranty; ;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.CategoryId = model.CategoryId;
                product.Category = await this.context.Categories.FirstOrDefaultAsync(x => x.Id == model.CategoryId);
                product.Description = model.Description;
                this.context.Products.Update(product);
                this.context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<Product>> GetAllProducts(
            string brand, string category, decimal priceFrom, decimal priceTo)
        {
            var result = this.context.Products.Include(c => c.Category).AsQueryable();

            if (brand != "" && brand != null)
            {
                result = result.Where(x => x.Brand == brand);
            }
            if (category != "" && category != null)
            {
                int id = Convert.ToInt32(category);
               result = result.Where(x => x.Category.Id == id);
            }
            if (priceFrom != 0 && priceTo != 0)
            {
                result = result.Where(x => x.Price >= priceFrom);
                result = result.Where(x => x.Price <= priceTo);
            }

            return await result.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await this.context.Products
                .Include(x => x.Category)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

        }

        public async Task<List<Product>> GetSearchedProducts(string searchParam)
        {
            return await this.context.Products
                .Include(x => x.Category)
                .Where(x => x.Brand.Contains(searchParam) || x.DeviceModel.Contains(searchParam))
                .ToListAsync();
        }
    }
}
