using System;
using AutoMapper;
using CommentService.CORE.Entities;
using CommentService.CORE.Interfaces.IRepositories;
using CommentService.CORE.Interfaces.IServices.ICommands;
using Shared.Contracts.DTOs.Comment.Requests;
using Shared.Contracts.DTOs.Comment.Responses;

namespace CommentService.BLL.Services.Commands;

public class CommentCommandService(
    ICommentRepository commentRepository,
    IMapper mapper
    ) : ICommentCommandService
{
    public async Task<CommentResponse> CreateAsync(
        CreateCommentRequest request,
        Guid userId,
        string username)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            AuthorId = userId,
            AuthorName = username,
            Text = request.Text,
            PostId = request.PostId
        };

        var createdComment = await commentRepository
            .CreateAsync(comment);

        return mapper.Map<CommentResponse>(createdComment);
    }

    public async Task RestoreDeletedUserCommentsAsync(Guid userId)
    {
        await commentRepository
            .RestoreDeletedUserCommentsAsync(userId);
    }

    public async Task SoftDeleteAllByPostIdsAsync(List<Guid> postIds)
    {
        await commentRepository
            .SoftDeleteAllByPostIdsAsync(postIds);
    }

    public async Task SoftDeleteUserCommentsAsync(Guid userId)
    {
        await commentRepository
            .SoftDeleteUserCommentsAsync(userId);
    }
}
