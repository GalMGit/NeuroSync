using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PostService.CORE.Entities;
using PostService.CORE.Interfaces.IRepositories;
using PostService.DAL.Database.DbFactory;

namespace PostService.DAL.Repositories;

public class PostRepository(DbFactory database) : IPostRepository
{
    private readonly IMongoCollection<Post> _posts = database
        .GetPostCollection("posts");

    public async Task<bool> CheckPostExistAsync(Guid postId)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.Id, postId)
                     & Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        return await _posts.Find(filter).AnyAsync();
    }

    public async Task<Post> CreateAsync(Post post)
    {
        await _posts.InsertOneAsync(post);
        return post;
    }

    public async Task ForceDeleteAsync(Guid id)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.Id, id);
        await _posts.DeleteOneAsync(filter);
    }

    public async Task<List<Post>> GetAllAsync()
    {
        var filter = Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        return await _posts.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Post>> GetAllByCommunityAsync(Guid communityId)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.CommunityId, communityId)
                     & Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        return await _posts.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Post>> GetAllByUserAsync(Guid userId)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.AuthorId, userId)
                     & Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        return await _posts.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.Id, id)
                     & Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        return await _posts.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Guid>> GetPostIdsByCommunityAsync(Guid communityId)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.CommunityId, communityId)
                     & Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        var posts = await _posts.Find(filter)
            .Project(x => x.Id)
            .ToListAsync();

        return posts;
    }

    public async Task RestoreUserPostsAsync(Guid userId)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.AuthorId, userId)
            & Builders<Post>.Filter.Eq(x => x.IsDeleted, true);

        var update = Builders<Post>.Update.Set(x => x.IsDeleted, false);

        await _posts.UpdateManyAsync(filter, update);
    }

    public async Task SoftDeleteAllByCommunityAsync(Guid communityId)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.CommunityId, communityId)
                     & Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        var update = Builders<Post>.Update.Set(x => x.IsDeleted, true);

        await _posts.UpdateManyAsync(filter, update);
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.Id, id)
                     & Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        var update = Builders<Post>.Update.Set(x => x.IsDeleted, true);

        await _posts.UpdateOneAsync(filter, update);
    }

    public async Task SoftDeleteUserPostsAsync(Guid userId)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.AuthorId, userId)
                     & Builders<Post>.Filter.Eq(x => x.IsDeleted, false);

        var update = Builders<Post>.Update.Set(x => x.IsDeleted, true);

        await _posts.UpdateManyAsync(filter, update);
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        var filter = Builders<Post>.Filter.Eq(x => x.Id, post.Id);

        await _posts.ReplaceOneAsync(filter, post);

        return post;
    }
}