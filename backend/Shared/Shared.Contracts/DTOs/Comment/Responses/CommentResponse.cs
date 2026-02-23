namespace Shared.Contracts.DTOs.Comment.Responses;

public class CommentResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }
    public string Text { get; set; }
    public Guid PostId { get; set; }
    public DateTime CreatedAt { get; set; }
}