using AggregateService.API.DTOs.Errors;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Interfaces.IPost;

public interface IPostGetServiceClient
{
    Task<IEnumerable<PostResponse>> GetUserPostsAsync();
    Task<PostResponse?> GetPostByIdAsync(Guid postId);
    Task<IEnumerable<PostResponse>> GetPostsByCommunityAsync(Guid communityId);
    Task<bool?> CheckPostExistAsync(Guid postId);
}