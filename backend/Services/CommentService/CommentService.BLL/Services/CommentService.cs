using AutoMapper;
using CommentService.CORE.Entities;
using CommentService.CORE.Interfaces.IRepositories;
using CommentService.CORE.Interfaces.IServices;
using CommentService.CORE.Interfaces.IServices.ICommands;
using CommentService.CORE.Interfaces.IServices.IQueries;
using Shared.Contracts.DTOs.Comment.Requests;
using Shared.Contracts.DTOs.Comment.Responses;

namespace CommentService.BLL.Services;

public class CommentService(
    ICommentCommandService commentCommandService,
    ICommentQueryService commentQueryService
    ) : ICommentService
{
    public async Task<CommentResponse> CreateAsync(
        CreateCommentRequest request,
        Guid userId,
        string username)
        => await commentCommandService.CreateAsync(request, userId, username);

    public async Task<CommentResponse?> GetByIdAsync(Guid id)
        => await commentQueryService.GetByIdAsync(id);

    public async Task<List<CommentResponse>> GetAllAsync()
        => await commentQueryService.GetAllAsync();

    public async Task<List<CommentResponse>> GetAllByPostAsync(Guid postId)
        => await commentQueryService.GetAllByPostAsync(postId);

    public async Task SoftDeleteUserCommentsAsync(Guid userId)
        => await commentCommandService.SoftDeleteUserCommentsAsync(userId);

    public async Task RestoreDeletedUserCommentsAsync(Guid userId)
        => await commentCommandService.RestoreDeletedUserCommentsAsync(userId);

    public async Task SoftDeleteAllByPostIdsAsync(List<Guid> postIds)
        => await commentCommandService.SoftDeleteAllByPostIdsAsync(postIds);
}