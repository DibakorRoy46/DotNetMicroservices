using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return new ShoppingCart();
            return JsonConvert.DeserializeObject<ShoppingCart>(basket) ?? new ShoppingCart();
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart cart)
        {
            await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
            return await GetBasketAsync(cart.UserName);
        }
    }
}
