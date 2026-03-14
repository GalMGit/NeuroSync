using System;
using Shared.Contracts.DTOs.Community.Responses;

namespace CommunityService.CORE.Interfaces.IServices.IQueries;

public interface ICommunityQueryService
{
    Task<CommunityResponse?> GetByIdAsync(Guid id);
    Task<List<CommunityResponse>> GetAllAsync();
    Task<List<CommunityResponse>> GetAllByUserAsync(Guid userId);
    Task<bool> CommunityExistAsync(Guid id);
}
