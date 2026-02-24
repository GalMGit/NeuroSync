using AggregateService.API.DTOs.Errors;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Interfaces;

public interface IPostServiceClient
{
    Task<ServiceResponse<IEnumerable<PostResponse>>> GetUserPostsAsync();
    Task<ServiceResponse<PostResponse>> GetPostByIdAsync(Guid postId);
}