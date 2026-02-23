using Shared.Contracts.DTOs.Comment.Responses;

namespace AggregateService.API.Services.Interfaces;

public interface ICommentServiceClient
{
    Task<IEnumerable<CommentResponse>?> GetCommentsByPostAsync(Guid postId);
}