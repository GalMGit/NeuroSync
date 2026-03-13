using System;
using Shared.Contracts.Enums;

namespace Shared.Contracts.DTOs.Auth.Responses.AuthUserResponse;

public class AuthUserResponse
{
    public Guid Id {get;set;}
    public string Username {get;set;}
    public bool EmailConfirmed {get;set;}
    public bool IsBanned {get;set;}
    public string AppRole {get;set;}
    public bool IsDeleted {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt {get;set;}
}
