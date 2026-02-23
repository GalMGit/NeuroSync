using Microsoft.EntityFrameworkCore;
using UserService.CORE.Entities;
using UserService.CORE.Interfaces.IRepositories;
using UserService.DAL.Database.DatabaseContext;

namespace UserService.DAL.Repositories;

public class UserRepository(
    UserDbContext database
    ) : IUserRepository
{
    public async Task CreateUserInfoAsync(User user)
    {
        await database.Users.AddAsync(user);
        await database.SaveChangesAsync();
    }

    public async Task<User?> GetUserInfoAsync(Guid userId)
    {
        return await database.Users
            .FirstOrDefaultAsync(x =>
                x.UserId == userId
                && !x.IsDeleted);
    }

    public async Task<User?> GetMyProfileAsync(Guid userId)
    {
        return await database.Users
            .FirstOrDefaultAsync(x =>
                x.UserId == userId
                && !x.IsDeleted);
    }
}