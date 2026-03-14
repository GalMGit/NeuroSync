using System;
using Shared.Contracts.DTOs.Post.Responses;

namespace PostService.CORE.Interfaces.IServices.IQueries;

public interface IPostQueryService
{
    Task<PostResponse?> GetByIdAsync(Guid id);
    Task<List<PostResponse>> GetAllAsync();
    Task<List<PostResponse>> GetAllByUserAsync(Guid userId);
    Task<List<PostResponse>> GetAllByCommunityAsync(Guid communityId);
    Task<bool> CheckPostExistAsync(Guid postId);
}
