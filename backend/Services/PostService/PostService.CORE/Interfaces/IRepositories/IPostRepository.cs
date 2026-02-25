using PostService.CORE.Entities;
using Shared.Abstractions.Interfaces;

namespace PostService.CORE.Interfaces.IRepositories;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>?> GetAllByUserAsync(Guid userId);
    Task<IEnumerable<Post>?> GetAllByCommunityAsync(Guid communityId);
}