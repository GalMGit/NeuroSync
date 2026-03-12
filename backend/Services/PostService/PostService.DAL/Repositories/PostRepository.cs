using Microsoft.EntityFrameworkCore;
using PostService.CORE.Entities;
using PostService.CORE.Interfaces.IRepositories;
using PostService.DAL.Database.DatabaseContext;

namespace PostService.DAL.Repositories;

public class PostRepository(
    PostDbContext database
    ) : IPostRepository
{
    public async Task<Post> CreateAsync(Post post)
    {
        await database.Posts.AddAsync(post);
        await database.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        return await database.Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Id == id
                && !x.IsDeleted);
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await database.Posts
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        database.Posts.Update(post);
        await database.SaveChangesAsync();
        return post;
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        await database.Posts
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, true));
    }

    public async Task ForceDeleteAsync(Guid id)
    {
        await database.Posts
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<List<Post>> GetAllByUserAsync(Guid userId)
    {
        return await database.Posts
            .AsNoTracking()
            .Where(x =>
                x.AuthorId == userId
                && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<List<Post>> GetAllByCommunityAsync(Guid communityId)
    {
        return await database.Posts
            .AsNoTracking()
            .Where(x =>
                x.CommunityId == communityId
                && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task SoftDeleteUserPostsAsync(Guid userId)
    {
        await database.Posts
            .Where(x =>
                x.AuthorId == userId
                && !x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, true));
    }

    public async Task RestoreUserPostsAsync(Guid userId)
    {
        await database.Posts
            .Where(x =>
                x.AuthorId == userId
                && x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, false));
    }

    public async Task SoftDeleteAllByCommunityAsync(Guid communityId)
    {
        await database.Posts
            .Where(x =>
                x.CommunityId == communityId
                && !x.IsDeleted)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(s =>
                    s.IsDeleted, true));
    }

    public async Task<List<Guid>> GetPostIdsByCommunityAsync(Guid communityId)
    {
        return await database.Posts
            .Where(x =>
                x.CommunityId == communityId
                && !x.IsDeleted)
            .Select(x => x.Id)
            .ToListAsync();
    }
}