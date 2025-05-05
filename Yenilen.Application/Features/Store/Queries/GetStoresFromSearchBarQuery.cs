using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Queries;

public sealed class GetStoresFromSearchBarQuery:IRequest<IQueryable<StoreDto>>
{
    public int TagId { get; set; }
    public double MinLatitude { get; set; }
    public double MaxLatitude { get; set; }
    public double MinLongitude { get; set; }
    public double MaxLongitude { get; set; }
    public DateTime Date { get; set; }
}