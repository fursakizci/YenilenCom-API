using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Queries;

public sealed class GetStoresQuery: IRequest<Result<List<GetStoresQueryResponse>>>
{
    public int? TagId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTime? Date { get; set; }
}

public sealed class GetStoresQueryResponse
{
    public string StoreId { get; set; }
    public string? Name { get; set; }
    public double? Rating  { get; set; }
    public int CountOfReview { get; set; }
    public string? ImageUrl { get; set; }
    public double Distance { get; set; }
    public string FullAddress { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    public List<ServiceDto> Services { get; set; }
}