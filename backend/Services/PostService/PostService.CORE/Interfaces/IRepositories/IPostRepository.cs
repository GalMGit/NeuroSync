using PostService.CORE.Entities;
using Shared.Abstractions.Interfaces;

namespace PostService.CORE.Interfaces.IRepositories;

public interface IPostRepository : IRepository<Post>
{
    Task<List<Post>> GetAllByUserAsync(Guid userId);
    Task<List<Post>> GetAllByCommunityAsync(Guid communityId);
    Task SoftDeleteUserPostsAsync(Guid userId);
    Task RestoreUserPostsAsync(Guid userId);
}