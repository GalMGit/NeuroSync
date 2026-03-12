using System;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Interfaces.IPost;

public interface IPostPostServiceClient
{
    Task<PostResponse?> CreatePostAsync(CreatePostRequest request);
}
