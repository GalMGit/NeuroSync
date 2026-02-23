namespace Shared.Contracts.DTOs.Comment.Requests;

public class CreateCommentRequest
{
    public string Text { get; set; }
    public Guid PostId { get; set; }
}