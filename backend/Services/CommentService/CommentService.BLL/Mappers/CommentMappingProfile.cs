using AutoMapper;
using CommentService.CORE.Entities;
using Shared.Contracts.DTOs.Comment.Responses;

namespace CommentService.BLL.Mappers;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<Comment, CommentResponse>();
    }
}