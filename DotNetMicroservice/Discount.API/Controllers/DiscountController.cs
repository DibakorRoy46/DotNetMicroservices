using Discount.API.Entities;
using Discount.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [HttpGet("{productName}", Name ="GetDiscount")]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscountAsync(productName);
            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> AddDiscount(Coupon coupon)
        {
            await _discountRepository.AddCouponAsync(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount(Coupon coupon)
        {
            return Ok(await _discountRepository.UpdateCouponAsync(coupon));
        }
        [HttpDelete("{productName}",Name ="DeleteDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> DeleteAsync(string productName)
        {
            return Ok(await _discountRepository.DeleteCouponAsync(productName));
        }
    }
}
