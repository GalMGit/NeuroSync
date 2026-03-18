using MongoDB.Driver;
using UserService.CORE.Entities;
using UserService.CORE.Interfaces.IRepositories;
using UserService.DAL.Database.DbFactory;

namespace UserService.DAL.Repositories;

public class UserRepository(DbFactory database) : IUserRepository
{
    private readonly IMongoCollection<User> _usersCollection
        = database.GetUsersCollection();

    public async Task<User> CreateAsync(User user)
    {
        await _usersCollection.InsertOneAsync(user);
        return user;
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Eq(u => u.UserId, userId),
            Builders<User>.Filter.Eq(u => u.IsDeleted, false)
        );

        return await _usersCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        var filter = Builders<User>.Filter.Eq(u => u.IsDeleted, false);
        return await _usersCollection.Find(filter).ToListAsync();
    }

    public async Task<User> UpdateAsync(User user)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Eq(u => u.UserId, user.UserId),
            Builders<User>.Filter.Eq(u => u.IsDeleted, false)
        );

        var options = new FindOneAndReplaceOptions<User>
        {
            ReturnDocument = ReturnDocument.After
        };

        var updatedUser = await _usersCollection
            .FindOneAndReplaceAsync(filter, user, options);

        return updatedUser;
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Eq(u => u.UserId, id),
            Builders<User>.Filter.Eq(u => u.IsDeleted, false)
        );

        var update = Builders<User>.Update
            .Set(u => u.IsDeleted, true)
            .Set(u => u.UpdatedAt, DateTime.UtcNow);

        var result = await _usersCollection
            .UpdateOneAsync(filter, update);
    }

    public async Task ForceDeleteAsync(Guid id)
    {
        var filter = Builders<User>.Filter.Eq(u => u.UserId, id);
        var result = await _usersCollection.DeleteOneAsync(filter);
    }

    public async Task RestoreUserAsync(Guid userId)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Eq(u => u.UserId, userId),
            Builders<User>.Filter.Eq(u => u.IsDeleted, true)
        );

        var update = Builders<User>.Update
            .Set(u => u.IsDeleted, false)
            .Set(u => u.UpdatedAt, DateTime.UtcNow);

        var result = await _usersCollection
            .UpdateOneAsync(filter, update);
    }
}