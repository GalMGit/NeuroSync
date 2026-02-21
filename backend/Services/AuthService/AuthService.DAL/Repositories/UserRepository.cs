using AuthService.CORE.Entities;
using AuthService.CORE.Interfaces.IRepositories;
using AuthService.DAL.Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DAL.Repositories;

public class UserRepository(
    AuthDbContext database
    ) : IUserRepository
{
    public async Task<User> CreateAsync(User entity)
    {
        await database.Users
            .AddAsync(entity);
        
        await database.SaveChangesAsync();
        
        return entity;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>?> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task ForceDeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await database.Users
            .Where(x => x.Email.Equals(email))
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await database.Users
            .AnyAsync(u =>
                u.Username.ToLower() == username.ToLower());
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await database.Users
            .AnyAsync(u =>
                u.Email.ToLower() == email.ToLower());
    }
}