using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product>GetProductAsync(string productId);
        Task<Product> GetProductByNameAsync(string productName);
        Task<IEnumerable<Product>>
            GetProductsByCategoryAsync(string categoryName);
        Task AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(string productId);
    }
}
