using CommentService.CORE.Entities;
using Shared.Abstractions.Interfaces;

namespace CommentService.CORE.Interfaces.IRepositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IEnumerable<Comment>?> GetAllByPostAsync(Guid postId);
}