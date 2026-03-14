using AutoMapper;
using Shared.Contracts.DTOs.User.Responses;
using Shared.Messaging.UserEvents;
using UserService.CORE.Entities;
using UserService.CORE.Interfaces.IRepositories;
using UserService.CORE.Interfaces.IServices;
using UserService.CORE.Interfaces.IServices.ICommands;
using UserService.CORE.Interfaces.IServices.IQueries;

namespace UserService.BLL.Services;

public class UserService(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService
) : IUserService
{
    public async Task CreateUserInfoAsync(UserCreatedEvent @event)
        => await userCommandService.CreateUserInfoAsync(@event);

    public async Task<UserProfileResponse?> GetUserProfileAsync(Guid userId)
        => await userQueryService.GetUserProfileAsync(userId);

    public async Task RestoreAccountAsync(Guid userId)
        => await userCommandService.RestoreAccountAsync(userId);

    public async Task SoftDeleteAsync(Guid userId)
        => await userCommandService.SoftDeleteAsync(userId);
}