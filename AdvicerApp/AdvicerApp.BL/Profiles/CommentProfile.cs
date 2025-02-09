using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.Core.Entities;
using AutoMapper;

namespace AdvicerApp.BL.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentCreateDto,Comment>().ReverseMap();
        CreateMap<CommentUpdateDto,Comment>().ReverseMap();
    }
}
