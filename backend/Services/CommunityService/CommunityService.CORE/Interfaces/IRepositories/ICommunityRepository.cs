using CommunityService.CORE.Entities;
using Shared.Abstractions.Interfaces;

namespace CommunityService.CORE.Interfaces.IRepositories;

public interface ICommunityRepository : IRepository<Community>
{
    Task<List<Community>> GetAllByUserAsync(Guid userId);
    Task SoftDeleteUserCommunities(Guid userId);
    Task RestoreDeletedUserCommunities(Guid userId);
    Task<bool> NameExistAsync(string name);
    Task<bool> CommunityExistAsync(Guid id);
}