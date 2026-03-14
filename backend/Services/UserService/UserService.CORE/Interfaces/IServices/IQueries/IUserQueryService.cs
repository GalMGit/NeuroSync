using System;
using Shared.Contracts.DTOs.User.Responses;

namespace UserService.CORE.Interfaces.IServices.IQueries;

public interface IUserQueryService
{
    Task<UserProfileResponse?> GetUserProfileAsync(Guid userId);
}
