using System;
using AggregateService.API.Services.Interfaces.IComment;
using AggregateService.API.Services.Interfaces.IPost;
using Shared.Contracts.DTOs.Comment.Requests;
using Shared.Contracts.DTOs.Comment.Responses;

namespace AggregateService.API.Services.Realisations.Comment;

public class CommentPostServiceClient(
    IPostGetServiceClient postGetServiceClient,
    IHttpClientFactory httpClientFactory
    ) : ICommentPostServiceClient
{
    private readonly HttpClient _httpClient = httpClientFactory
        .CreateClient("CommentService");

    public async Task<CommentResponse?> CreateCommentAsync(CreateCommentRequest request)
    {
        var postExist = await postGetServiceClient
            .CheckPostExistAsync(request.PostId);

        if(postExist != true)
        {
            throw new KeyNotFoundException("Пост не существует");
        }
        else
        {
            var response = await _httpClient
                .PostAsJsonAsync($"/api/comments", request);

            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<CommentResponse>();

                return result;
            }

            return null;
        }
    }
}
