using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Auth.Queries;

public sealed class CheckIsExistUserByEmailQuery : IRequest<Result<CheckIsExistUserByEmailQueryResponse>>
{
    public string Email { get; set; } = string.Empty;
}

public sealed class CheckIsExistUserByEmailQueryResponse
{
    public bool isEmailExist { get; set; }
}

public class CheckIsExistUserByEmailQueryValidator : AbstractValidator<CheckIsExistUserByEmailQuery>
{
    public CheckIsExistUserByEmailQueryValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email alani bos olamaz.")
            .EmailAddress().WithMessage("Email formatina uygun olmali.");
    }
}