using CommunityService.CORE.Entities;
using Shared.Abstractions.Interfaces;

namespace CommunityService.CORE.Interfaces.IRepositories;

public interface ICommunityRepository : IRepository<Community>
{
    Task<IEnumerable<Community>?> GetAllByUserAsync(Guid userId);
}