using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.User.Queries;

public sealed class GetAllFavouritesByUserIdQuery:IRequest<Result<List<GetAllFavouritesByUserIdQueryResponse>>>
{
    public int UserId { get; set; }
}

public sealed class GetAllFavouritesByUserIdQueryResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Rating { get; set; }
    public int ReviewCount { get; set; }
    public string ImageUrl { get; set; }
    public string FullAddress { get; set; }
    public string BusinessTag { get; set; }
}
