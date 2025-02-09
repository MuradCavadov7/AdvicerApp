using AdvicerApp.BL.DTOs.MenuItemDtos;
using AdvicerApp.Core.Entities;
using AutoMapper;

namespace AdvicerApp.BL.Profiles;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItemCreateDto, MenuItem>().ReverseMap();
        CreateMap<MenuItemUpdateDto, MenuItem>().ReverseMap();
    }
}
