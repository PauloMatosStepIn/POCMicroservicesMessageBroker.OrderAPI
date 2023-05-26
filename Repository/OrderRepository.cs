using StepIn.Services.OrderAPI.DbContexts;
using StepIn.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using StepIn.Services.OrderAPI.Messages;
using AutoMapper;

namespace StepIn.Services.OrderAPI.Repository
{
  public class OrderRepository : IOrderRepository
  {
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;

    private IMapper _mapper;

    public OrderRepository(DbContextOptions<ApplicationDbContext> dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public async Task<bool> AddCoupon(CouponDto couponDto)
    {
      try
      {
        Coupon coupon = _mapper.Map<Coupon>(couponDto);
        await using var _db = new ApplicationDbContext(_dbContext);
        _db.Coupons.Add(coupon);
        await _db.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return false;
      }

      return true;
    }

    public async Task<IEnumerable<CouponDto>> GetCoupons()
    {
      try
      {
        await using var _db = new ApplicationDbContext(_dbContext);
        List<Coupon> couponList = await _db.Coupons.OrderByDescending(c => c.CouponDate).ToListAsync();
        return _mapper.Map<List<CouponDto>>(couponList);
      }
      catch (Exception ex)
      {
        return null;
      }

    }
  }
}
