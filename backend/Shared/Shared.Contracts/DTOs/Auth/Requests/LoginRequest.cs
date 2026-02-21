using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.DTOs.Auth.Requests;

public class LoginRequest
{
    [EmailAddress]
    [Required]
    [MaxLength(30)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(20)]
    [MinLength(5)]
    public string Password { get; set; }
}