using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Auth.Commands;

public sealed class LoginCommand:IRequest<Result<LoginCommandResponse>>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; }
}

public sealed class LoginCommandResponse
{
    public string Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
    public IReadOnlyList<AddressDto>? Addresses { get; set; }
}