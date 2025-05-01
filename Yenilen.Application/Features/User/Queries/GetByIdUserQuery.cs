using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.User.Queries;

public sealed class GetByIdUserQuery:IRequest<UserDto>
{
    public int UserId { get; }

    public GetByIdUserQuery(int userId)
    {
        UserId = userId;
    }
}

