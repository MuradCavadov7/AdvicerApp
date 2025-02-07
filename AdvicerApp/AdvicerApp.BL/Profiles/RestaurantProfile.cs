using AdvicerApp.BL.DTOs.RestaurantDtos;
using AdvicerApp.Core.Entities;
using AutoMapper;

namespace AdvicerApp.BL.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantDto,Restaurant>().ReverseMap();
        CreateMap<UpdateResturantDto, Restaurant>().ReverseMap();
    }
}
