using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Features.Category.Commands;

public sealed class CreateCategoryCommand: IRequest<Result<CreateCategoryCommandResponse>>
{
    public string Name { get; set; } = default!;
}

public sealed class CreateCategoryCommandResponse
{
    public int CategoryId { get; set; }
}

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad alanı boş olamaz.")
            .MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır.");
    }
}

