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
            .FirstOrDefaultAsync(x =>
                x.Id == id
                && !x.IsDeleted);
    }

    public async Task<IEnumerable<Post>?> GetAllAsync()
    {
        return await database.Posts
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public Task<Post> UpdateAsync(Post entity)
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

    public async Task<IEnumerable<Post>?> GetAllByUserAsync(Guid userId)
    {
        return await database.Posts
            .Where(x =>
                x.AuthorId == userId
                && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>?> GetAllByCommunityAsync(Guid communityId)
    {
        return await database.Posts
            .Where(x =>
                x.CommunityId == communityId
                && !x.IsDeleted)
            .ToListAsync();
    }
}