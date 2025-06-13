using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Tag.Queries;

public sealed class GetAllTagsQuery :IRequest<Result<List<GetAllTagsQueryResponse>>>
{
    
}

public sealed class GetAllTagsQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}