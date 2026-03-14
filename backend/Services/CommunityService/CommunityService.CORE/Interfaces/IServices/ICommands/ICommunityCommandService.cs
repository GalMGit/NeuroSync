using System;
using Shared.Contracts.DTOs.Community.Requests;
using Shared.Contracts.DTOs.Community.Responses;

namespace CommunityService.CORE.Interfaces.IServices.ICommands;

public interface ICommunityCommandService
{
    Task<CommunityResponse> CreateAsync(
        CreateCommunityRequest request,
        Guid userId,
        string username);

    Task SoftDeleteUserCommunities(Guid userId);
    Task RestoreDeletedUserCommunities(Guid userId);
    Task SoftDeleteAsync(Guid id);
}
