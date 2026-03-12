using CommentService.CORE.Entities;
using Shared.Abstractions.Interfaces;

namespace CommentService.CORE.Interfaces.IRepositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<List<Comment>> GetAllByPostAsync(Guid postId);
    Task SoftDeleteUserCommentsAsync(Guid userId);
    Task RestoreDeletedUserCommentsAsync(Guid userId);
    Task SoftDeleteAllByPostIdsAsync(List<Guid> postIds);
}