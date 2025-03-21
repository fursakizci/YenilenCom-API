using FluentValidation;
using MediatR;

namespace Yenilen.Application.Features.Users.Commands;

public class CreateUserCommand:IRequest<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Sex { get; set; }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ad alani en az 3 karakter olmalidir.");
        RuleFor(x => x.Surname).MinimumLength(3).WithMessage("Soyad alani en az 3 karakter olmalidir.");
    }
}