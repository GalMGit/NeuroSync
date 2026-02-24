namespace Shared.Contracts.DTOs.Community.Responses;

public class CommunityResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public string? Description { get; set; }
    public string OwnerName { get; set; }
    public DateTime CreatedAt { get; set; }
}