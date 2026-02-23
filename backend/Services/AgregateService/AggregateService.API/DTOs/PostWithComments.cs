using Shared.Contracts.DTOs.Comment.Responses;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.DTOs;

public class PostWithComments
{
    public PostResponse Post { get; set; }
    public IEnumerable<CommentResponse> Comments { get; set; } = [];
}