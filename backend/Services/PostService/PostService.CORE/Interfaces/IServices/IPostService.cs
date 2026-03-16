using Shared.Contracts.DTOs.Post.Requests;
using Shared.Contracts.DTOs.Post.Responses;

namespace PostService.CORE.Interfaces.IServices;

public interface IPostService
{
    Task<PostResponse> CreateAsync(
        CreatePostRequest request,
        string username,
        Guid userId);
    Task<PostResponse?> GetByIdAsync(Guid id);
    Task<List<PostResponse>> GetAllAsync();
    Task<List<PostResponse>> GetAllByUserAsync(Guid userId);
    Task<List<PostResponse>> GetAllByCommunityAsync(Guid communityId);
    Task SoftDeleteUserPostsAsync(Guid userId);
    Task RestoreUserPostsAsync(Guid userId);
    Task SoftDeleteAsync(Guid postId, Guid userId);
    Task ForceDeleteAsync(Guid postId);
    Task<PostResponse> UpdatePostAsync(
        Guid postId,
        Guid userId,
        UpdatePostRequest request);

    Task SoftDeleteAllByCommunity(Guid communityId);
    Task<bool> CheckPostExistAsync(Guid postId);

}