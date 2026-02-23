using System;

namespace Shared.Contracts.DTOs.User.Responses;

public class UserProfileResponse
{
    public Guid UserId { get;set; }
    public string DisplayName { get;set; }
    public string? AvatarUrl { get;set; }
    public int PostCount { get;set; }
}
