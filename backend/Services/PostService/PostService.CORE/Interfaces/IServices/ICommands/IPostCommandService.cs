using System;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.Contracts.DTOs.Post.Responses;

namespace PostService.CORE.Interfaces.IServices.ICommands;

public interface IPostCommandService
{
    Task<PostResponse> CreateAsync(
        CreatePostRequest request,
        string username,
        Guid userId);
    Task SoftDeleteUserPostsAsync(Guid userId);
    Task RestoreUserPostsAsync(Guid userId);
    Task SoftDeleteAsync(Guid postId, Guid userId);
    Task ForceDeleteAsync(Guid postId);
    Task<PostResponse> UpdatePostAsync(
        Guid postId,
        Guid userId,
        UpdatePostRequest request);

    Task SoftDeleteAllByCommunity(Guid communityId);
}
