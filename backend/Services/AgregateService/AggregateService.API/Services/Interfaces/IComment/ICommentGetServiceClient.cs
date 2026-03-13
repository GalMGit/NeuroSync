using AggregateService.API.DTOs.Errors;
using Shared.Contracts.DTOs.Comment.Responses;

namespace AggregateService.API.Services.Interfaces.IComment;

public interface ICommentGetServiceClient
{
    Task<IEnumerable<CommentResponse>> GetCommentsByPostAsync(Guid postId);
}