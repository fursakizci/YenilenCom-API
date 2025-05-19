using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Category.Queries;

public sealed class GetCategoriesByStoreIdQuery: IRequest<Result<List<GetCategoriesByStoreIdQueryResponse>>>
{
    public int StoreId { get; set; }
}

public sealed class GetCategoriesByStoreIdQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ServiceDto> Services { get; set; }
}