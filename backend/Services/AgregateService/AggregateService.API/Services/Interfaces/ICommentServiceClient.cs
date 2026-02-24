using AggregateService.API.DTOs.Errors;
using Shared.Contracts.DTOs.Comment.Responses;

namespace AggregateService.API.Services.Interfaces;

public interface ICommentServiceClient
{
    Task<ServiceResponse<IEnumerable<CommentResponse>>> GetCommentsByPostAsync(Guid postId);
}