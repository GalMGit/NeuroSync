using CommunityService.CORE.Interfaces.IServices;
using CommunityService.CORE.Interfaces.IServices.ICommands;
using CommunityService.CORE.Interfaces.IServices.IQueries;
using Shared.Contracts.DTOs.Community.Requests;
using Shared.Contracts.DTOs.Community.Responses;

namespace CommunityService.BLL.Services;

public class CommunityService(
    ICommunityQueryService communityQueryService,
    ICommunityCommandService communityCommandService
    ) : ICommunityService
{
    public async Task<CommunityResponse> CreateAsync(
        CreateCommunityRequest request,
        Guid userId,
        string username)
    => await communityCommandService.CreateAsync(request, userId, username);

    public async Task<CommunityResponse?> GetByIdAsync(Guid id)
        => await communityQueryService.GetByIdAsync(id);

    public async Task<List<CommunityResponse>> GetAllAsync()
        => await communityQueryService.GetAllAsync();

    public async Task<List<CommunityResponse>> GetAllByUserAsync(Guid userId)
        => await communityQueryService.GetAllByUserAsync(userId);

    public async Task SoftDeleteUserCommunities(Guid userId)
        => await communityCommandService.SoftDeleteUserCommunities(userId);

    public async Task RestoreDeletedUserCommunities(Guid userId)
        => await communityCommandService.RestoreDeletedUserCommunities(userId);

    public async Task<bool> CommunityExistAsync(Guid id)
        => await communityQueryService.CommunityExistAsync(id);

    public async Task SoftDeleteAsync(Guid id)
        => await communityCommandService.SoftDeleteAsync(id);
}