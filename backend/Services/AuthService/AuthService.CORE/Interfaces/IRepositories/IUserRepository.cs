using AuthService.CORE.Entities;
using Shared.Abstractions.Interfaces; 
namespace AuthService.CORE.Interfaces.IRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
}