using StepIn.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StepIn.Services.OrderAPI.DbContexts
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Coupon> Coupons { get; set; }
  }
}
