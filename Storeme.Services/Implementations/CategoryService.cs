using Microsoft.EntityFrameworkCore;
using Storeme.Data;
using Storeme.Entities;
using Storeme.Services.Contracts;

namespace Storeme.Services.Implementations
{
    public class CategoryService : DataService, ICategoryService
    {
        public CategoryService(StoremeDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetCategories()
        {
            return await this.context.Categories.ToListAsync();
        }
    }
}
