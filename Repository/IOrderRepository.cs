using StepIn.Services.OrderAPI.Messages;
using StepIn.Services.OrderAPI.Models;

namespace StepIn.Services.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddCoupon(CouponDto coupon);

        Task<IEnumerable<CouponDto>> GetCoupons();
    }
}
