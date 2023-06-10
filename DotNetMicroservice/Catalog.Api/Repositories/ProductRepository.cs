using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProductAsync(string productId)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, productId);

            var deleteResult = await _context.Products
                                          .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Find(x=>true).ToListAsync();
        }

        public async Task<Product> GetProductAsync(string productId)
        {
            return await _context.Products
                                 .Find(x => x.Id == productId)
                                 .FirstOrDefaultAsync();

        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter
                                                            .Eq(x => x.Name, productName);
            return await _context.Products
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter
                                                             .Eq(x => x.Category, categoryName);
            return await _context.Products
                                 .Find(filter)
                                 .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult=await _context.Products
                                           .ReplaceOneAsync(filter:x=>x.Id==product.Id,
                                            replacement:product);
            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
