using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.DTOs.Community.Requests;

public class CreateCommunityRequest
{
    [Required(ErrorMessage = "Название не может быть пустым")]
    [MaxLength(40, ErrorMessage = "Название должно иметь менее 40 символов")]
    public string Name { get; set; }

    [MaxLength(200, ErrorMessage = "Описание не может иметь более 200 символов")]
    public string? Description { get; set; }
}
