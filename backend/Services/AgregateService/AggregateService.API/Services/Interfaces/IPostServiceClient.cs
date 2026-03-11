using AggregateService.API.DTOs.Errors;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Interfaces;

public interface IPostServiceClient
{
    Task<IEnumerable<PostResponse>> GetUserPostsAsync();
    Task<PostResponse> GetPostByIdAsync(Guid postId);
    Task<IEnumerable<PostResponse>> GetPostsByCommunityAsync(Guid communityId);
}