using Shared.Abstractions.Interfaces;
using UserService.CORE.Entities;

namespace UserService.CORE.Interfaces.IRepositories;

public interface IUserRepository : IRepository<User>
{
    Task RestoreUserAsync(Guid userId);
}