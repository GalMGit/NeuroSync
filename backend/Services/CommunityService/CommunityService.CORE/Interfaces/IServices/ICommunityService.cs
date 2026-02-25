using Shared.Contracts.DTOs.Community.Requests;
using Shared.Contracts.DTOs.Community.Responses;

namespace CommunityService.CORE.Interfaces.IServices;

public interface ICommunityService
{
    Task<CommunityResponse> CreateAsync(
        CreateCommunityRequest request,
        Guid userId,
        string username);
    Task<CommunityResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<CommunityResponse>?> GetAllAsync();
    Task<IEnumerable<CommunityResponse>?> GetAllByUserAsync(Guid userId);
}