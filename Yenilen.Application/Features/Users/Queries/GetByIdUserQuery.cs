using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Users.Queries;

public class GetByIdUserQuery :IRequest<UserDto>
{
    public Guid UserId { get; }

    public GetByIdUserQuery(Guid userId)
    {
        UserId = userId;
    }
}

