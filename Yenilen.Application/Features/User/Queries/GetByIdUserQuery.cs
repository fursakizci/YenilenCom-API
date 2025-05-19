using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.User.Queries;

public sealed class GetByIdUserQuery:IRequest<Result<GetByIdUserQueryResponse>>
{
    public int UserId { get; }
}

public sealed class GetByIdUserQueryResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
}