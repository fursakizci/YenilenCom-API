using MediatR;
using TS.Result;

namespace Yenilen.Application.Auth.Commands;

public sealed class StoreLoginCommand : IRequest<Result<StoreLoginCommandResponse>>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; }
}

public sealed class StoreLoginCommandResponse
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
}

