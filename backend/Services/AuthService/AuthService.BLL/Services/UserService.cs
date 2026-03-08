using AuthService.CORE.Entities;
using AuthService.CORE.Interfaces.IRepositories;
using AuthService.CORE.Interfaces.IServices;
using AutoMapper;
using MassTransit;
using Shared.Contracts.DTOs.Auth.Requests;
using Shared.Contracts.DTOs.Auth.Responses;
using Shared.Messaging.UserEvents;

namespace AuthService.BLL.Services;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
    IPublishEndpoint publishEndpoint,
    IMapper mapper
    ) : IUserService
{
    public async Task<RegisterResponse> CreateAsync(
        RegisterRequest request)
    {
        if (await userRepository.UsernameExistsAsync(request.Username))
            throw new Exception("Пользователь с таким username существует");

        if (await userRepository.EmailExistsAsync(request.Email))
            throw new Exception("Пользователь с таким email существует");

        var user = new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Email = request.Email,
            Username = request.Username,
            PasswordHash = passwordHasher
                .GenerateHash(request.Password)
        };

        var createdUser = await userRepository
            .CreateAsync(user);

        await publishEndpoint.Publish(new UserCreatedEvent
        {
            UserId = createdUser.Id,
            Username = createdUser.Username
        });

        return mapper.Map<RegisterResponse>(createdUser);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await userRepository
            .GetByEmailAsync(request.Email);
        
        if (user is null || !passwordHasher
                .VerifyHash(request.Password, user.PasswordHash))
            throw new Exception("Неверный логин или пароль");

        if (user.IsDeleted)
            throw new Exception("Пользователь не найден");
        
        var token = jwtProvider
            .GenerateToken(user);

        return new LoginResponse
        {
            Token = token
        };
    }

    public async Task RestoreAccountAsync(LoginRequest request)
    {
        var user = await userRepository
                       .GetByEmailAsync(request.Email) ??
                   throw new Exception("Нет такого пользователя");

        if (!user.IsDeleted)
            throw new Exception("Пользователь не удален");

        if (passwordHasher.VerifyHash(request.Password, user.PasswordHash))
        {
            await userRepository
                .RestoreUserAsync(user.Id);

            await publishEndpoint.Publish(new UserRestoredEvent
            {
                UserId = user.Id
            });
        }
        else
        {
            throw new Exception("Неверные данные");
        }
    }

    public async Task SoftDeleteAsync(Guid userId)
    {
        var user = await userRepository
            .GetByIdAsync(userId);

        if(user is null || user.IsDeleted)
        {
            throw new Exception("Пользователь не найден");
        }
    
        if(!user.IsDeleted)
        {
            await userRepository
                .SoftDeleteAsync(userId);

            await publishEndpoint.Publish(new UserDeletedEvent
            {
                UserId = userId
            });
        }
    }
}