using AuthService.CORE.Entities;
using AuthService.CORE.Interfaces.IRepositories;
using AuthService.CORE.Interfaces.IServices;
using AutoMapper;
using Shared.Contracts.DTOs.Auth.Requests;
using Shared.Contracts.DTOs.Auth.Responses;

namespace AuthService.BLL.Services;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
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

        return mapper.Map<RegisterResponse>(createdUser);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await userRepository
            .GetByEmailAsync(request.Email);

        if (user is null || !passwordHasher
                .VerifyHash(request.Password, user.PasswordHash))
            throw new Exception("Неверный логин или пароль");

        var token = jwtProvider
            .GenerateToken(user);

        return new LoginResponse
        {
            Token = token
        };
    }
}