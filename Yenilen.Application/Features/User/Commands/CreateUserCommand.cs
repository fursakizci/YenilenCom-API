using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Features.Users.Commands;

public sealed class CreateUserCommand:IRequest<Result<CreateUserCommandResponse>>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string PhoneNumber { get; set; }= string.Empty;
    public string Email { get; set; }= string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }= string.Empty;
}

public sealed class CreateUserCommandResponse
{
    public int UserId { get; set; }
}

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad alanı boş olamaz.")
            .MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır.");

        RuleFor(x => x.LastName)
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

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Cinsiyet alanı zorunludur.")
            .Must(x => x.Equals("Male", StringComparison.OrdinalIgnoreCase) ||
                       x.Equals("Female", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Cinsiyet 'Erkek' veya 'Kadın' olmalıdır.");
    }
}