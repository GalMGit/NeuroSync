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

    public async Task<IEnumerable<Community>?> GetAllAsync()
    {
        return await database.Communities
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public Task<Community> UpdateAsync(Community entity)
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

    public async Task<IEnumerable<Community>?> GetAllByUserAsync(Guid userId)
    {
        return await database.Communities
            .Include(x => x.CommunityMembers)
            .Where(x => x.CommunityMembers
                .Any(c => 
                    c.UserId == userId))
            .ToListAsync();
    }
}