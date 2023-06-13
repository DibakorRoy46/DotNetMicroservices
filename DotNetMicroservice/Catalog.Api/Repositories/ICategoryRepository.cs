using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(string categoryId);
        Task AddCategoryAsync(Category category);
    }
}
