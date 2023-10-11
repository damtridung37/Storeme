using Storeme.Entities;

namespace Storeme.Services.Contracts
{
    public interface IProductService
    {
        Task<bool> CreateProduct(Product product);

        Task<List<Product>> GetAllProducts(
            string brand, string category, decimal priceFrom, decimal priceTo);

        Task<Product> GetProduct(int id);

        Task<bool> DeleteProduct(int id);

        Task<bool> UpdateProduct(Product product);

        Task<List<Product>> GetSearchedProducts(string searchParam);
    }
}
