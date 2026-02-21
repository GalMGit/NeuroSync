using AuthService.CORE.Entities;
using AutoMapper;
using Shared.Contracts.DTOs.Auth.Responses;

namespace AuthService.BLL.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, RegisterResponse>()
            .ForMember(x => x.Email,
                x =>
                    x.MapFrom(s => s.Email));
    }
}