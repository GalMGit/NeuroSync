using System;
using Shared.Contracts.DTOs.Community.Responses;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.DTOs;

public class CommunityWithPosts
{
    public CommunityResponse Community {get;set;}
    public IEnumerable<PostResponse> Posts {get;set;} = [];
}
