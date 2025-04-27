using FluentValidation;
using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Commands;

public class CreateStoreCommand:IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    //public string WebsiteUrl { get; set; } = string.Empty;
    public List<int>? TagIds { get; set; }
    public string ManagerName { get; set; }
    public string ManagerPhone { get; set; }
    public AddressDto Address { get; set; }

    //public string CountOfStaff { get; set; } = string.Empty;
    //public string OwnerName { get; set; } = string.Empty;
    public List<StoreWorkingHourDto> StoreWorkingHours { get; set; } = new();
}

public sealed class CrateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
{
    public CrateStoreCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Mağaza adı boş olamaz.")
            .MaximumLength(50).WithMessage("İşletme adı 50 karakterden fazla olamaz.");

        RuleFor(x => x.TagIds)
            .NotEmpty().WithMessage("En az bir kategori seçilmeli");

        // RuleFor(x => x.CountOfStaff)
        //     .NotEmpty().WithMessage("Lütfen çalışan sayınızı belirtin.");
        //
        // RuleFor(x => x.OwnerName)
        //     .NotEmpty().WithMessage("İşletme sahibinin adı zorunludur.")
        //     .MaximumLength(100).WithMessage("İsim alanı 100 karakterden fazla olamaz.");
    }
}