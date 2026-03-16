using AutoMapper;
using MassTransit;
using PostService.CORE.Entities;
using PostService.CORE.Interfaces.IRepositories;
using PostService.CORE.Interfaces.IServices;
using PostService.CORE.Interfaces.IServices.ICommands;
using PostService.CORE.Interfaces.IServices.IQueries;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.Contracts.DTOs.Post.Responses;
using Shared.Messaging.PostEvents;

namespace PostService.BLL.Services;

public class PostService(
    IPostCommandService postCommandService,
    IPostQueryService postQueryService
    ) : IPostService
{
    public async Task<PostResponse> CreateAsync(
        CreatePostRequest request,
        string username,
        Guid userId)
        => await postCommandService.CreateAsync(request, username, userId);

    public async Task<PostResponse?> GetByIdAsync(Guid id)
        => await postQueryService.GetByIdAsync(id);

    public async Task<List<PostResponse>> GetAllAsync()
        => await postQueryService.GetAllAsync();

    public async Task<List<PostResponse>> GetAllByUserAsync(Guid userId)
        => await postQueryService.GetAllByUserAsync(userId);

    public async Task<List<PostResponse>> GetAllByCommunityAsync(Guid communityId)
        => await postQueryService.GetAllByCommunityAsync(communityId);

    public async Task SoftDeleteUserPostsAsync(Guid userId)
        => await postCommandService.SoftDeleteUserPostsAsync(userId);

    public async Task RestoreUserPostsAsync(Guid userId)
        => await postCommandService.RestoreUserPostsAsync(userId);

    public async Task SoftDeleteAsync(Guid postId, Guid userId)
        => await postCommandService.SoftDeleteAsync(postId, userId);

    public async Task ForceDeleteAsync(Guid postId)
        => await postCommandService.ForceDeleteAsync(postId);

    public async Task<PostResponse> UpdatePostAsync(
        Guid postId,
        Guid userId,
        UpdatePostRequest request)
        => await postCommandService.UpdatePostAsync(postId, userId, request);

    public async Task SoftDeleteAllByCommunity(Guid communityId)
        => await postCommandService.SoftDeleteAllByCommunity(communityId);

    public async Task<bool> CheckPostExistAsync(Guid postId)
        => await postQueryService.CheckPostExistAsync(postId);
}