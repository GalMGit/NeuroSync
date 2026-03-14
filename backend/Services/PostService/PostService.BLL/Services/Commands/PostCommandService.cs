using System;
using AutoMapper;
using PostService.CORE.Entities;
using PostService.CORE.Interfaces.IRepositories;
using PostService.CORE.Interfaces.IServices.ICommands;
using PostService.CORE.Interfaces.IServices.IEvents;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.Contracts.DTOs.Post.Responses;

namespace PostService.BLL.Services.Commands;

public class PostCommandService(
    IPostRepository postRepository,
    IPostEventService postEventService,
    IMapper mapper) : IPostCommandService
{
    public async Task<PostResponse> CreateAsync(
        CreatePostRequest request,
        Guid userId)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            AuthorId = userId,
            CommunityId = request.CommunityId,
            PosterUrl = request.PosterUrl,
            Title = request.Title,
            Description = request.Description
        };

        var createdPost = await postRepository
            .CreateAsync(post);

        return mapper.Map<PostResponse>(createdPost);
    }

    public async Task ForceDeleteAsync(Guid postId)
    {
        await postRepository
            .ForceDeleteAsync(postId);
    }

    public async Task RestoreUserPostsAsync(Guid userId)
    {
        await postRepository
            .RestoreUserPostsAsync(userId);
    }

    public async Task SoftDeleteAllByCommunity(Guid communityId)
    {
        var postIds = await postRepository
            .GetPostIdsByCommunityAsync(communityId);

        await postRepository
            .SoftDeleteAllByCommunityAsync(communityId);

        await postEventService
            .PublishPostsDeletedAsync(postIds);
    }

    public async Task SoftDeleteAsync(Guid postId, Guid userId)
    {
        var post = await postRepository
                       .GetByIdAsync(postId)
                   ?? throw new Exception("Пост не найден");

        if (post.AuthorId != userId)
            throw new UnauthorizedAccessException();

        await postRepository
            .SoftDeleteAsync(postId);
    }

    public async Task SoftDeleteUserPostsAsync(Guid userId)
    {
        await postRepository
            .SoftDeleteUserPostsAsync(userId);
    }

    public async Task<PostResponse> UpdatePostAsync(
        Guid postId,
        Guid userId,
        UpdatePostRequest request)
    {
        var existsPost = await postRepository
            .GetByIdAsync(postId)
                         ?? throw new Exception("Пост не найден");

        if(existsPost.AuthorId != userId)
            throw new UnauthorizedAccessException();

        if (!string.IsNullOrWhiteSpace(request.Title))
            existsPost.Title = request.Title;

        if (!string.IsNullOrWhiteSpace(request.Description))
            existsPost.Description = request.Description;

        existsPost.PosterUrl = request.PosterUrl;

        existsPost.UpdatedAt = DateTime.UtcNow;

        var updatedPost = await postRepository
            .UpdateAsync(existsPost);

        return mapper.Map<PostResponse>(updatedPost);

    }
}
