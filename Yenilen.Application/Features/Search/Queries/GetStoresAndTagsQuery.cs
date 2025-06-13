using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Search.Queries;

public sealed class GetStoresAndTagsQuery : IRequest<Result<GetStoresAndTagsResponse>>
{
    public string? QueryString { get; set; }
}

public sealed class GetStoresAndTagsResponse
{
    public List<TagDto>? Tags { get; set; }
    public List<StoreSearchDto>? Stores { get; set; }
}