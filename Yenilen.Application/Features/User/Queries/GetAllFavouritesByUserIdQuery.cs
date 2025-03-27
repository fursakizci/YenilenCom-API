using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.User.Queries;

public class GetAllFavouritesByUserIdQuery:IRequest<List<FavouriteDto>>
{
    public int UserId { get; set; }

    public GetAllFavouritesByUserIdQuery(int userId)
    {
        UserId = userId;
    }
}