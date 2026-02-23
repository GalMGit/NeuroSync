using UserService.CORE.Entities;

namespace UserService.CORE.Interfaces.IRepositories;

public interface IUserRepository
{
    Task CreateUserInfoAsync(User user);
    Task<User?> GetUserInfoAsync(Guid userId);
    Task<User?> GetMyProfileAsync(Guid userId);
}