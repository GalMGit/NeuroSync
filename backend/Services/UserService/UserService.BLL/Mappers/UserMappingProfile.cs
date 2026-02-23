using AutoMapper;
using Shared.Contracts.DTOs.User.Responses;
using UserService.CORE.Entities;

namespace UserService.BLL.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserProfileResponse>();
    }
}