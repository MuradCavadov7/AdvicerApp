using AdvicerApp.BL.DTOs.MenuDtos;
using AdvicerApp.Core.Entities;
using AutoMapper;

namespace AdvicerApp.BL.Profiles;

public class MenuProfile : Profile
{
    public MenuProfile()
    {
        CreateMap<MenuCreateDto, Menu>().ReverseMap();
        CreateMap<MenuUpdateDto, Menu>().ReverseMap();
    }
}
