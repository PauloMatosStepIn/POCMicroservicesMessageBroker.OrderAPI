
using System.ComponentModel.DataAnnotations;

namespace StepIn.Services.OrderAPI.Models
{
  public class Coupon
  {
    [Key]
    public int Id { get; set; }
    public string CouponCode { get; set; }
    public DateTime CouponDate { get; set; }
  }
}
