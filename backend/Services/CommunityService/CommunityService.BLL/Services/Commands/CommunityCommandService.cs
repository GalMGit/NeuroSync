using System;
using AutoMapper;
using CommunityService.CORE.Entities;
using CommunityService.CORE.Interfaces.IRepositories;
using CommunityService.CORE.Interfaces.IServices.ICommands;
using CommunityService.CORE.Interfaces.IServices.IEvents;
using CommunityService.CORE.Interfaces.IServices.IQueries;
using Shared.Contracts.DTOs.Community.Requests;
using Shared.Contracts.DTOs.Community.Responses;
using Shared.Contracts.Enums;

namespace CommunityService.BLL.Services.Commands;

public class CommunityCommandService(
    ICommunityRepository communityRepository,
    ICommunityEventService communityEventService,
    ICommunityQueryService communityQueryService,
    IMapper mapper
    ) : ICommunityCommandService
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

        if(await communityRepository
            .NameExistAsync(community.Name))
                throw new ArgumentException("Сообщество с таким именом существует");

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

    public async Task RestoreDeletedUserCommunities(Guid userId)
    {
        await communityRepository
            .RestoreDeletedUserCommunities(userId);
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        if(!await communityQueryService.CommunityExistAsync(id))
        {
            throw new KeyNotFoundException("Сообщество не существует");
        }

        await communityRepository
            .SoftDeleteAsync(id);

        await communityEventService
            .PublishCommunityDeletedAsync(id);
    }

    public async Task SoftDeleteUserCommunities(Guid userId)
    {
        await communityRepository
            .SoftDeleteUserCommunities(userId);
    }
}
