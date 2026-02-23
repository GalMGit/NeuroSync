using Shared.Abstractions.Models;

namespace PostService.CORE.Entities;

public class Post : BaseEntity
{
    public Guid AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? PosterUrl { get; set; }
}