using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Auth.Commands;

public sealed class RefreshCommand:IRequest<Result<RefreshCommandResponse>>
{
    public string RefreshToken { get; set; }
}

public sealed class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty().WithMessage("Refresh token alani bos olamaz.");
    }
}

public class RefreshCommandResponse
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
}