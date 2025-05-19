using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Queries;

public sealed class GetStoresFromSearchBarQuery:IRequest<Result<IQueryable<GetStoresFromSearchBarQueryResponse>>>
{
    public int TagId { get; set; }
    public double MinLatitude { get; set; }
    public double MaxLatitude { get; set; }
    public double MinLongitude { get; set; }
    public double MaxLongitude { get; set; }
    public DateTime Date { get; set; }
}

public sealed class GetStoresFromSearchBarQueryResponse
{
    public int StoreId { get; set; }
    public string? Name { get; set; }
    public double? Rating  { get; set; }
    public int CountOfReview { get; set; }
    public List<string>? ImageUrls { get; set; }
    public double Distance { get; set; }
    public AddressDto Address { get; set; }
    public List<ServiceDto> Services { get; set; }
}