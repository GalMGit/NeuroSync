using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Interfaces;
using UserService.CORE.Entities;
using UserService.CORE.Interfaces.IRepositories;
using UserService.DAL.Database.DatabaseContext;

namespace UserService.DAL.Repositories;

public class UserRepository(
    UserDbContext database
    ) : IUserRepository
{
    public async Task<User> CreateAsync(User user)
    {
        await database.Users.AddAsync(user);
        await database.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await database.Users
            .FirstOrDefaultAsync(x =>
                x.UserId == userId);
    }

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        await database.Users
            .Where(x =>
                x.UserId == id
                && !x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, true));
    }

    public Task ForceDeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task RestoreUserAsync(Guid userId)
    {
        await database.Users
            .Where(x =>
                x.UserId == userId
                && x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, false));
    }
}