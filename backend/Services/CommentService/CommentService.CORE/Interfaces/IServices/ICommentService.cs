using Shared.Contracts.DTOs.Comment.Requests;
using Shared.Contracts.DTOs.Comment.Responses;

namespace CommentService.CORE.Interfaces.IServices;

public interface ICommentService
{
    Task<CommentResponse> CreateAsync(
        CreateCommentRequest request,
        Guid userId,
        string username);
    Task<CommentResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<CommentResponse>?> GetAllAsync();
    Task<IEnumerable<CommentResponse>?> GetAllByPostAsync(Guid postId);
}