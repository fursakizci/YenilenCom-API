using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Auth.Commands;

public class RegisterCommand:IRequest<Result<RegisterCommandResponse>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public sealed class RegisterCommandResponse
{
    public int UserId { get; set; }
    //public string AccessToken { get; set; }
}

public sealed class CreateRegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public CreateRegisterCommandValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("Isim alani dolu olmali.");
            

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Soyad alani dolu olmali.");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email alani bos olamaz.")
            .EmailAddress().WithMessage("Email formatina uygun olmali.");

        RuleFor(r => r.PhoneNumber)
            .NotEmpty().WithMessage("Telefon Numarasi alani bos olamaz.")
            .Matches(@"^[0-9]{10,}$").WithMessage("Telefon numarasini gecerli formatta olmalidir");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Sifre alani bos olamaz.")
            .MinimumLength(8).WithMessage("sifre en az 8 karakter olmalidir");

        RuleFor(r => r.ConfirmPassword)
            .Equal(r => r.Password).WithMessage("Şifre ve şifre tekrarı aynı olmalıdır.");
    }
}