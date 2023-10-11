using Storeme.Entities;

namespace Storeme.Services.Contracts
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
    }
}
