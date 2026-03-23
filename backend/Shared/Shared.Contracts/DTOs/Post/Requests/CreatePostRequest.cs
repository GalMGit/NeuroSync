using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.DTOs.Post.Requests;

public class CreatePostRequest
{
    [Required(ErrorMessage = "Название не может быть пустым")]
    [MaxLength(100, ErrorMessage = "Название не может иметь более 100 символов")]
    [MinLength(3, ErrorMessage = "Название не может иметь менее 3 символв")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Content не может быть пустым")]
    [MaxLength(1000, ErrorMessage = "Content не может иметь более 1000 символов")]
    public string Content { get; set; }

    public string? PosterUrl { get; set; }
    public Guid? CommunityId { get; set; }
}