using MediatR;
using TS.Result;

namespace Yenilen.Application.Auth.Commands;

public sealed class LogoutCommand : IRequest<Result<LogoutCommandResponse>>
{
}

public sealed class LogoutCommandResponse
{
}