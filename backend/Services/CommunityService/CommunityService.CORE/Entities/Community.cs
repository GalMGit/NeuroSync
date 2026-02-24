using Shared.Abstractions.Models;

namespace CommunityService.CORE.Entities;

public class Community : BaseEntity
{
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<CommunityMember> CommunityMembers { get; set; } = [];

}