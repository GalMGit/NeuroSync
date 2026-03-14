using AutoMapper;
using CommentService.CORE.Interfaces.IRepositories;
using CommentService.CORE.Interfaces.IServices.IQueries;
using Shared.Contracts.DTOs.Comment.Responses;

namespace CommentService.BLL.Services.Queries;

public class CommentQueryService(
    ICommentRepository commentRepository,
    IMapper mapper
    ) : ICommentQueryService
{
    public Task<List<CommentResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<CommentResponse>> GetAllByPostAsync(Guid postId)
    {
        var comments = await commentRepository
            .GetAllByPostAsync(postId);

        return mapper.Map<List<CommentResponse>>(comments);
    }

    public Task<CommentResponse?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
