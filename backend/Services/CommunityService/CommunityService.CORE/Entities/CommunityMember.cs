using Shared.Abstractions.Models;
using Shared.Contracts.Enums;

namespace CommunityService.CORE.Entities;

public class CommunityMember : BaseEntity
{
    public Guid UserId { get; set; }
    public string MemberName { get; set; }
    public Guid CommunityId { get; set; }
    public Community Community { get; set; }
    public CommunityRole CommunityRole { get; set; }
}