using FluentValidation;
using MediatR;

namespace Yenilen.Application.Features.Users.Commands;

public class CreateUserCommand:IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; }= string.Empty;
    public string PhoneNumber { get; set; }= string.Empty;
    public string Email { get; set; }= string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Sex { get; set; }= string.Empty;
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad alanı boş olamaz.")
            .MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Soyad alanı boş olamaz.")
            .MinimumLength(3).WithMessage("Soyad alanı en az 3 karakter olmalıdır.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası zorunludur.")
            .Matches(@"^[0-9]{10,}$").WithMessage("Telefon numarası geçerli formatta olmalıdır. Örneğin, en az 10 rakam içermelidir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email alanı zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email adresi girin.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("Doğum tarihi gelecekte olamaz.");

        RuleFor(x => x.Sex)
            .NotEmpty().WithMessage("Cinsiyet alanı zorunludur.")
            .Must(x => x.Equals("Erkek", StringComparison.OrdinalIgnoreCase) ||
                       x.Equals("Kadın", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Cinsiyet 'Erkek' veya 'Kadın' olmalıdır.");
    }
}