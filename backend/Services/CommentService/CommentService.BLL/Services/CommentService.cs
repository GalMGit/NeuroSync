using AutoMapper;
using CommentService.CORE.Entities;
using CommentService.CORE.Interfaces.IRepositories;
using CommentService.CORE.Interfaces.IServices;
using Shared.Contracts.DTOs.Comment.Requests;
using Shared.Contracts.DTOs.Comment.Responses;

namespace CommentService.BLL.Services;

public class CommentService(
    ICommentRepository commentRepository,
    IMapper mapper
    ) : ICommentService
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

    public Task<CommentResponse?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<CommentResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<CommentResponse>> GetAllByPostAsync(Guid postId)
    {
        var comments = await commentRepository
            .GetAllByPostAsync(postId);

        return mapper.Map<List<CommentResponse>>(comments);
    }

    public async Task SoftDeleteUserCommentsAsync(Guid userId)
    {
        await commentRepository
            .SoftDeleteUserCommentsAsync(userId);
    }

    public async Task RestoreDeletesUserCommentsAsync(Guid userId)
    {
        await commentRepository
            .RestoreDeletedUserCommentsAsync(userId);
    }

    public Task RestoreDeletedUserCommentsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task SoftDeleteAllByPostIdsAsync(List<Guid> postIds)
    {
        await commentRepository
            .SoftDeleteAllByPostIdsAsync(postIds);
    }
}