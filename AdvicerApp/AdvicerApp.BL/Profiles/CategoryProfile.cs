using AdvicerApp.BL.DTOs.CategoryDtos;
using AdvicerApp.Core.Entities;
using AutoMapper;

namespace AdvicerApp.BL.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<CategoryUpdateDto, Category>().ReverseMap();
    }
}
