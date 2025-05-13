using MediatR;
using TS.Result;

namespace Yenilen.Application.Auth.Commands;

public sealed class LoginCommand:IRequest<Result<LoginCommandResponse>>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; }
}

public sealed class LoginCommandResponse
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
}