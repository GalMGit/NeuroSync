using Shared.Contracts.Enums;

namespace AuthService.CORE.Entities;

public record User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsBanned { get; set; }
    public AppRole AppRole { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}