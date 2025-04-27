using FluentValidation;
using MediatR;

namespace Yenilen.Application.Features.Tag.Commands;

public sealed class CreateTagCommand:IRequest<int>
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
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