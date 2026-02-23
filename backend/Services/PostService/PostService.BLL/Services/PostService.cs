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

    public async Task<IEnumerable<PostResponse>?> GetAllAsync()
    {
        var posts = await postRepository
            .GetAllAsync();

        return mapper.Map<IEnumerable<PostResponse>>(posts);
    }

    public async Task<IEnumerable<PostResponse>?> GetAllByUserAsync(Guid userId)
    {
        var posts = await postRepository
            .GetAllByUserAsync(userId);

        return mapper.Map<IEnumerable<PostResponse>>(posts);
    }
}