using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Category.Queries;

public sealed class GetCategoriesByStoreIdQuery: IRequest<Result<List<GetCategoriesByStoreIdQueryResponse>>>
{
    public string StoreId { get; set; }
}

public sealed class GetCategoriesByStoreIdQueryResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<ServiceDto> Services { get; set; }
}