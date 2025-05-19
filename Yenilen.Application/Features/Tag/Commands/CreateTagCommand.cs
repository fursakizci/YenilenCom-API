using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Features.Tag.Commands;

public sealed class CreateTagCommand:IRequest<Result<CreateTagCommandResponse>>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}

public sealed class CreateTagCommandResponse
{
    public int tagId { get; set; }
}

public sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad alani bos olamaz.");
        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("url bos olamaz.");

    }
}