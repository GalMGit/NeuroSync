using System;
using Shared.Contracts.DTOs.Auth.Responses.AuthUserResponse;

namespace AuthService.CORE.Interfaces.IServices.IQueries;

public interface IUserQueryService
{
    Task<AuthUserResponse?> GetByIdAsync(Guid id);
    Task<List<AuthUserResponse>> GetAllAsync();
    Task<AuthUserResponse?> GetByEmailAsync(string email);
}
