using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Features.StaffMember.Commands;

public sealed class CreateStaffMemberCommand:IRequest<Result<CreateStaffMemberCommandResponse>>
{
    public string StoreId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    //public DateTime? DateOfBirth { get; set; }
    //public DateTime StartDate { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
}

public sealed class CreateStaffMemberCommandResponse
{
    public int StaffId { get; set; }
}

public sealed class CreateStaffMemberCommandValidator : AbstractValidator<CreateStaffMemberCommand>
{
    public CreateStaffMemberCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad alanı boş olamaz.")
            .MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır.");
    
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email alanı zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email adresi girin.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası zorunludur.")
            .Matches(@"^[0-9]{10,}$").WithMessage("Telefon numarası geçerli formatta olmalıdır. Örneğin, en az 10 rakam içermelidir.");

        // RuleFor(x => x.DateOfBirth)
        //     .LessThan(DateTime.Now).WithMessage("Doğum tarihi gelecekte olamaz.");

        RuleFor(x => x.Bio)
            .MaximumLength(1000).WithMessage("Hakkinda alani 1000 karakterden fazla olamaz.");
    }
}