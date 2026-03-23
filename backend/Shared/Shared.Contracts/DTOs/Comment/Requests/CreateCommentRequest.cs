using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.DTOs.Comment.Requests;

public class CreateCommentRequest
{
    [Required(ErrorMessage = "Text не может быть пустым")]
    [MaxLength(500, ErrorMessage = "Text не должен иметь более 500 символов")]
    public string Text { get; set; }

    public Guid PostId { get; set; }
}
