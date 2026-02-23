using Shared.Abstractions.Models;

namespace CommentService.CORE.Entities;

public class Comment : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid AuthorId { get; set; }
    public string Text { get; set; }
    public string AuthorName { get; set; }
}