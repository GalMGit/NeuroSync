using AutoMapper;
using CommunityService.CORE.Entities;
using CommunityService.CORE.Interfaces.IRepositories;
using CommunityService.CORE.Interfaces.IServices;
using Shared.Contracts.DTOs.Community.Requests;
using Shared.Contracts.DTOs.Community.Responses;
using Shared.Contracts.Enums;

namespace CommunityService.BLL.Services;

public class CommunityService(
    ICommunityRepository communityRepository,
    IMapper mapper
    ) : ICommunityService
{
    public async Task<CommunityResponse> CreateAsync(
        CreateCommunityRequest request,
        Guid userId,
        string username)
    {
        var community = new Community
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Description = request.Description,
            Name = request.Name,
            OwnerId = userId,
            OwnerName = username
        };
        
        community.CommunityMembers.Add(new CommunityMember
        {
            Id = Guid.NewGuid(),
            CreatedAt = community.CreatedAt,
            CommunityId = community.Id,
            CommunityRole = CommunityRole.Creator,
            MemberName = community.OwnerName,
            UserId = community.OwnerId
        });

        var createdCommunity = await communityRepository
            .CreateAsync(community);

        return mapper.Map<CommunityResponse>(createdCommunity);
    }

    public async Task<CommunityResponse?> GetByIdAsync(Guid id)
    {
        var community = await communityRepository
            .GetByIdAsync(id);

        return mapper.Map<CommunityResponse>(community);
    }

    public async Task<IEnumerable<CommunityResponse>?> GetAllAsync()
    {
        var communities = await communityRepository
            .GetAllAsync();

        return mapper.Map<IEnumerable<CommunityResponse>>(communities);
    }

    public async Task<IEnumerable<CommunityResponse>?> GetAllByUserAsync(Guid userId)
    {
        var communities = await communityRepository
            .GetAllByUserAsync(userId);

        return mapper.Map<IEnumerable<CommunityResponse>>(communities);
    }
}