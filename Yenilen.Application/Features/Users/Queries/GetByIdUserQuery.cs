using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Users.Queries;

public class GetByIdUserQuery :IRequest<UserDto>
{
    public int UserId { get; }

    public GetByIdUserQuery(int userId)
    {
        UserId = userId;
    }
}

