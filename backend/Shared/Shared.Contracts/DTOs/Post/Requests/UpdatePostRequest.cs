using System;

namespace Shared.Contracts.DTOs.Post.Requests;

public class UpdatePostRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
}
