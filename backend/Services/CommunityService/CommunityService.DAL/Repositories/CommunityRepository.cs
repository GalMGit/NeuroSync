using CommunityService.CORE.Entities;
using CommunityService.CORE.Interfaces.IRepositories;
using CommunityService.DAL.Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.DAL.Repositories;

public class CommunityRepository(
    CommunityDbContext database
    ) : ICommunityRepository
{
    public async Task<Community> CreateAsync(Community community)
    {
        await database.Communities.AddAsync(community);
        await database.SaveChangesAsync();
        return community;
    }

    public async Task<Community?> GetByIdAsync(Guid id)
    {
        return await database.Communities
            .FirstOrDefaultAsync(x =>
                x.Id == id
                && !x.IsDeleted);
    }

    public async Task<List<Community>> GetAllAsync()
    {
        return await database.Communities
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public Task<Community> UpdateAsync(Community entity)
    {
        throw new NotImplementedException();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        await database.Communities
            .Where(x =>
                x.Id == id
                && !x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, true));

        await database.CommunityMembers
            .Where(x =>
                x.CommunityId == id
                && !x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                s.IsDeleted, true));
    }

    public Task ForceDeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Community>> GetAllByUserAsync(Guid userId)
    {
        return await database.Communities
            .Include(x => x.CommunityMembers)
            .Where(x => x.CommunityMembers
                .Any(c =>
                    c.UserId == userId))
            .ToListAsync();
    }

    public async Task SoftDeleteUserCommunities(Guid userId)
    {
        await database.Communities
            .Where(x =>
                x.OwnerId == userId
                && !x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, true));
    }

    public async Task RestoreDeletedUserCommunities(Guid userId)
    {
        await database.Communities
            .Where(x =>
                x.OwnerId == userId
                && x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, false));
    }

    public async Task<bool> NameExistAsync(string name)
    {
        return await database.Communities
            .AnyAsync(x => x.Name
                .ToLower()
                .Equals(name.ToLower()));
    }

    public async Task<bool> CommunityExistAsync(Guid id)
    {
        return await database.Communities
            .AnyAsync(x =>
                x.Id == id
                && !x.IsDeleted);
    }
}