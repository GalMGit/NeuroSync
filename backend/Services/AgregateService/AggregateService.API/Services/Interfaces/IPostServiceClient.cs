using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Interfaces;

public interface IPostServiceClient
{
    Task<IEnumerable<PostResponse>?> GetUserPostsAsync();
    Task<PostResponse?> GetPostByIdAsync(Guid postId);
}