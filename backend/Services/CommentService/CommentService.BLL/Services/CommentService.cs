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

    public Task<IEnumerable<CommentResponse>?> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CommentResponse>?> GetAllByPostAsync(Guid postId)
    {
        var comments = await commentRepository
            .GetAllByPostAsync(postId);

        return mapper.Map<IEnumerable<CommentResponse>>(comments);
    }
}