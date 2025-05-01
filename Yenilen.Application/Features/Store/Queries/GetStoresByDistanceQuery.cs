using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Queries;

public sealed class GetStoresByDistanceQuery:IRequest<List<StoreDto>>
{
    public double MinLatitude { get; set; }
    public double MaxLatitude { get; set; }
    public double MinLongitude { get; set; }
    public double MaxLongitude { get; set; }
    public int? TagId { get; set; }
}