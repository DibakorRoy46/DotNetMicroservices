using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ICatalogContext _context;
        public CategoryRepository(ICatalogContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.InsertOneAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Find(x => true).ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(string categoryId)
        {
            return await _context.Categories
                                 .Find(x => x.Id == categoryId)
                                 .FirstOrDefaultAsync();
        }
    }
}
