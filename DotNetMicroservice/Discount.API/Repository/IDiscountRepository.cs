using Discount.API.Entities;

namespace Discount.API.Repository
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscountAsync(string couponName);
        Task<bool> AddCouponAsync(Coupon coupon);
        Task<bool> UpdateCouponAsync(Coupon coupon);
        Task<bool> DeleteCouponAsync(string couponName);    
    }
}
