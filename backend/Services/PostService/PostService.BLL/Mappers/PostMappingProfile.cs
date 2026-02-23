using AutoMapper;
using PostService.CORE.Entities;
using Shared.Contracts.DTOs.Post.Responses;

namespace PostService.BLL.Mappers;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, PostResponse>();
    }
}