using AggregateService.API.DTOs.Errors;
using Shared.Contracts.DTOs.User.Responses;

namespace AggregateService.API.Services.Interfaces;

public interface IUserServiceClient
{
    Task<ServiceResponse<UserProfileResponse>> GetProfileAsync();
}