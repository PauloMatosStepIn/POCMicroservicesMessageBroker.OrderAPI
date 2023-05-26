using AutoMapper;
using StepIn.Services.OrderAPI.Models;
using StepIn.Services.OrderAPI.Messages;


namespace StepIn.Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
