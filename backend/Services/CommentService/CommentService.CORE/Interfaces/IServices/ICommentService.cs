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
    Task<List<CommentResponse>> GetAllAsync();
    Task<List<CommentResponse>> GetAllByPostAsync(Guid postId);
    Task SoftDeleteUserCommentsAsync(Guid userId);
    Task RestoreDeletedUserCommentsAsync(Guid userId);
    Task SoftDeleteAllByPostIdsAsync(List<Guid> postIds);
}