namespace Shared.Contracts.DTOs.Post.Responses;

public class PostResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? PosterUrl { get; set; }
}