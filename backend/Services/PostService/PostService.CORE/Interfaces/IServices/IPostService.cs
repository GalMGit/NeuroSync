using Shared.Contracts.DTOs.Post.Requests;
using Shared.Contracts.DTOs.Post.Responses;

namespace PostService.CORE.Interfaces.IServices;

public interface IPostService
{
    Task<PostResponse> CreateAsync(CreatePostRequest request, Guid userId);
    Task<PostResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<PostResponse>?> GetAllAsync();
    Task<IEnumerable<PostResponse>?> GetAllByUserAsync(Guid userId);
    Task<IEnumerable<PostResponse>?> GetAllByCommunityAsync(Guid communityId);
}