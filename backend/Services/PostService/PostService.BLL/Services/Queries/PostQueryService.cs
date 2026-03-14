using System;
using AutoMapper;
using PostService.CORE.Interfaces.IRepositories;
using PostService.CORE.Interfaces.IServices.IQueries;
using Shared.Contracts.DTOs.Post.Responses;

namespace PostService.BLL.Services.Queries;

public class PostQueryService(
    IPostRepository postRepository,
    IMapper mapper
    ) : IPostQueryService
{
    public async Task<bool> CheckPostExistAsync(Guid postId)
    {
        return await postRepository
            .CheckPostExistAsync(postId);
    }

    public async Task<List<PostResponse>> GetAllAsync()
    {
        var posts = await postRepository
            .GetAllAsync();

        return mapper.Map<List<PostResponse>>(posts);
    }

    public async Task<List<PostResponse>> GetAllByCommunityAsync(Guid communityId)
    {
        var posts = await postRepository
            .GetAllByCommunityAsync(communityId);

        return mapper.Map<List<PostResponse>>(posts);
    }

    public async Task<List<PostResponse>> GetAllByUserAsync(Guid userId)
    {
        var posts = await postRepository
            .GetAllByUserAsync(userId);

        return mapper.Map<List<PostResponse>>(posts);
    }

    public async Task<PostResponse?> GetByIdAsync(Guid id)
    {
        var post = await postRepository
            .GetByIdAsync(id);

        return mapper.Map<PostResponse>(post);
    }
}
