using Shared.Abstractions.Models;

namespace UserService.CORE.Entities;

public class User : BaseEntity
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public int PostCount { get; set; }
}