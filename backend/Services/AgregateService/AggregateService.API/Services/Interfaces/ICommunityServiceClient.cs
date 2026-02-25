using System;
using AggregateService.API.DTOs.Errors;
using Shared.Contracts.DTOs.Community.Responses;

namespace AggregateService.API.Services.Interfaces;

public interface ICommunityServiceClient
{
    Task<ServiceResponse<CommunityResponse>> GetCommunityAsync(Guid communityId);
}
