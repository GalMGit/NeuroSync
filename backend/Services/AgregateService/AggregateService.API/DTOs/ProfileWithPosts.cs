using Shared.Contracts.DTOs.Post.Responses;
using Shared.Contracts.DTOs.User.Responses;

namespace AggregateService.API.DTOs;

public class ProfileWithPosts
{
    public UserProfileResponse Profile { get; set; }
    public IEnumerable<PostResponse> Posts { get; set; } = [];
}