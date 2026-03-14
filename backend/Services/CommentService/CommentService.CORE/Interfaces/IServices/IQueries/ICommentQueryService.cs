using System;
using Shared.Contracts.DTOs.Comment.Responses;

namespace CommentService.CORE.Interfaces.IServices.IQueries;

public interface ICommentQueryService
{
    Task<CommentResponse?> GetByIdAsync(Guid id);
    Task<List<CommentResponse>> GetAllAsync();
    Task<List<CommentResponse>> GetAllByPostAsync(Guid postId);
}
