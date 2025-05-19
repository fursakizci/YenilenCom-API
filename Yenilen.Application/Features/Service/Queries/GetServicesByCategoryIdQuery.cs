using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Service.Queries;

public class GetServicesByCategoryIdQuery:IRequest<Result<List<GetServicesByCategoryIdQueryResponse>>>
{
    public int CategoryId { get; set; }
}

public class GetServicesByCategoryIdQueryResponse
{
    public string CategoryId { get; set; }
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Duration { get; set; }
    public int MaxInSecond { get; set; } = 0;
    public int MinInSecond { get; set; } = 0;
}