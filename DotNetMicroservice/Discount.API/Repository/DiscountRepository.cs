using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> AddCouponAsync(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var success = await connection.ExecuteAsync
                ("insert into coupon (ProductName,Description,Amount) values (@ProductName,@Description,@Amount)",
                new { ProductName = coupon.ProductName, Description=coupon.Description,Amount=coupon.Amount });

            if (success == 0)
                return false;
            return true;
        }

        public async Task<bool> DeleteCouponAsync(string couponName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var success = await connection.ExecuteAsync
                ("delete from Coupon where ProductName=@ProductName", new { ProductName = couponName });

            if (success == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscountAsync(string couponName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("select * from Coupon where ProductName=@ProductName", new { ProductName = couponName });

            if (coupon == null)
                return new Coupon()
                {
                    ProductName = "No Discount",
                    Description = "NO Discount Desc",
                    Amount = 0
                };
            return coupon;
          
        }

        public async Task<bool> UpdateCouponAsync(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var success = await connection.ExecuteAsync
                ("update coupon set ProductName=@ProductName,Description=@Description,Amount=@Amount where Id=@Id ",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount,Id=coupon.Id });

            if (success == 0)
                return false;
            return true;
        }
    }
}
