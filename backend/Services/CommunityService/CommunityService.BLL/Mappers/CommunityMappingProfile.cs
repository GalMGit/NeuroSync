using AutoMapper;
using CommunityService.CORE.Entities;
using Shared.Contracts.DTOs.Community.Responses;

namespace CommunityService.BLL.Mappers;

public class CommunityMappingProfile : Profile
{
    public CommunityMappingProfile()
    {
        CreateMap<Community, CommunityResponse>();
    }
}