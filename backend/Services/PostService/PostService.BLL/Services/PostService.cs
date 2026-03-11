using AutoMapper;
using PostService.CORE.Entities;
using PostService.CORE.Interfaces.IRepositories;
using PostService.CORE.Interfaces.IServices;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.Contracts.DTOs.Post.Responses;

namespace PostService.BLL.Services;

public class PostService(
    IPostRepository postRepository,
    IMapper mapper
    ) : IPostService
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

    public async Task<PostResponse?> GetByIdAsync(Guid id)
    {
        var post = await postRepository
            .GetByIdAsync(id);

        return mapper.Map<PostResponse>(post);
    }

    public async Task<List<PostResponse>> GetAllAsync()
    {
        var posts = await postRepository
            .GetAllAsync();

        return mapper.Map<List<PostResponse>>(posts);
    }

    public async Task<List<PostResponse>> GetAllByUserAsync(Guid userId)
    {
        var posts = await postRepository
            .GetAllByUserAsync(userId);

        return mapper.Map<List<PostResponse>>(posts);
    }

    public async Task<List<PostResponse>> GetAllByCommunityAsync(Guid communityId)
    {
        var posts = await postRepository
            .GetAllByCommunityAsync(communityId);

        return mapper.Map<List<PostResponse>>(posts);
    }

    public async Task SoftDeleteUserPostsAsync(Guid userId)
    {
        await postRepository
            .SoftDeleteUserPostsAsync(userId);
    }

    public async Task RestoreUserPostsAsync(Guid userId)
    {
        await postRepository
            .RestoreUserPostsAsync(userId);
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

    public async Task ForceDeleteAsync(Guid postId)
    {
        await postRepository
            .ForceDeleteAsync(postId);
    }

    public async Task<PostResponse> UpdatePostAsync(
        Guid postId,
        UpdatePostRequest request)
    {
        var existsPost = await postRepository
            .GetByIdAsync(postId)
                         ?? throw new Exception("Пост не найден");

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