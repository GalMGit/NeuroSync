using System;
using Shared.Contracts.DTOs.Comment.Requests;
using Shared.Contracts.DTOs.Comment.Responses;

namespace AggregateService.API.Services.Interfaces.IComment;

public interface ICommentPostServiceClient
{
    Task<CommentResponse?> CreateCommentAsync(CreateCommentRequest request);
}
