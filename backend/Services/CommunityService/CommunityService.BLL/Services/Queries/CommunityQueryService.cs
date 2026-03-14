using System;
using AutoMapper;
using CommunityService.CORE.Interfaces.IRepositories;
using CommunityService.CORE.Interfaces.IServices.IQueries;
using Shared.Contracts.DTOs.Community.Responses;

namespace CommunityService.BLL.Services.Queries;

public class CommunityQueryService(
    ICommunityRepository communityRepository,
    IMapper mapper
    ) : ICommunityQueryService
{
    public async Task<bool> CommunityExistAsync(Guid id)
    {
        return await communityRepository
            .CommunityExistAsync(id);
    }

    public async Task<List<CommunityResponse>> GetAllAsync()
    {
        var communities = await communityRepository
            .GetAllAsync();

        return mapper.Map<List<CommunityResponse>>(communities);
    }

    public async Task<List<CommunityResponse>> GetAllByUserAsync(Guid userId)
    {
        var communities = await communityRepository
            .GetAllByUserAsync(userId);

        return mapper.Map<List<CommunityResponse>>(communities);
    }

    public async Task<CommunityResponse?> GetByIdAsync(Guid id)
    {
        var community = await communityRepository
            .GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Сообщество не найдено");

        return mapper.Map<CommunityResponse>(community);
    }
}
