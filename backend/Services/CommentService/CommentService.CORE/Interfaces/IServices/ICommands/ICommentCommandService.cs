using System;
using Shared.Contracts.DTOs.Comment.Requests;
using Shared.Contracts.DTOs.Comment.Responses;

namespace CommentService.CORE.Interfaces.IServices.ICommands;

public interface ICommentCommandService
{
    Task<CommentResponse> CreateAsync(
        CreateCommentRequest request,
        Guid userId,
        string username);
    Task SoftDeleteUserCommentsAsync(Guid userId);
    Task RestoreDeletedUserCommentsAsync(Guid userId);
    Task SoftDeleteAllByPostIdsAsync(List<Guid> postIds);
}
