using Shared.Contracts.Enums;
using Shared.Abstractions;
using Shared.Abstractions.Models;
namespace AuthService.CORE.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsBanned { get; set; }
    public AppRole AppRole { get; set; }
    
}