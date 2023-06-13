using Basket.API.Entities;

namespace Basket.API.Repository
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName);
        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart cart);
        Task DeleteBasketAsync(string userName);
    }
}
