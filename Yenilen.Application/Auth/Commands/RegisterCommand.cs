using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Auth.Commands;

public sealed class RegisterCommand:IRequest<Result<RegisterCommandResponse>>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public string? CompanyName { get; set; } = string.Empty;
    
}

public sealed class RegisterCommandResponse
{
    public string UserId { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
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
        
        RuleFor(r => r.Role)
            .NotEmpty().WithMessage("Rol alani bos olamaz.");
    }
}