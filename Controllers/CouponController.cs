using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StepIn.Services.OrderAPI.Messages;
using StepIn.Services.OrderAPI.Repository;

namespace StepIn.Services.OrderAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CouponController : ControllerBase
  {
    protected ResponseDto _response;
    private IOrderRepository _orderRepository;

    public CouponController(IOrderRepository orderRepository)
    {
      _orderRepository = orderRepository;
      this._response = new ResponseDto();
    }

    [HttpGet]
    public async Task<Object> Get()
    {

      try
      {
        IEnumerable<CouponDto> couponDtos = await _orderRepository.GetCoupons();
        _response.Result = couponDtos;
      }
      catch (Exception ex)
      {
        _response.IsSuccess = false;
        _response.ErrorMessages = new List<string>() { ex.ToString() };
      }
      return _response;
    }

  }
}