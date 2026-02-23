using CommentService.CORE.Entities;
using CommentService.CORE.Interfaces.IRepositories;
using CommentService.DAL.Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CommentService.DAL.Repositories;

public class CommentRepository(
    CommentDbContext database
    ) : ICommentRepository
{
    public async Task<Comment> CreateAsync(Comment comment)
    {
        await database.Comments.AddAsync(comment);
        await database.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        return await database.Comments
            .FirstOrDefaultAsync(x =>
                x.Id == id
                && !x.IsDeleted);
    }

    public async Task<IEnumerable<Comment>?> GetAllAsync()
    {
        return await database.Comments
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public Task<Comment> UpdateAsync(Comment entity)
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

    public async Task<IEnumerable<Comment>?> GetAllByPostAsync(Guid postId)
    {
        return await database.Comments
            .Where(x =>
                x.PostId == postId
                && !x.IsDeleted)
            .ToListAsync();
    }
}