using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.DTOs.Auth.Requests;

public class LoginRequest
{
    [EmailAddress(ErrorMessage = "Поле Email обязательно должно быть почтой")]
    [MaxLength(50, ErrorMessage = "Email не должен иметь более 50 символов")]
    [Required(ErrorMessage = "Email не может быть пустым")]
    public string Email { get; set; }

    [MaxLength(20, ErrorMessage = "Password не должен иметь более 20 символов")]
    [MinLength(5, ErrorMessage = "Password не должен иметь менее 5 символов")]
    [Required(ErrorMessage = "Password не может быть пустым")]
    public string Password { get; set; }
}