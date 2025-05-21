using FluentValidation;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Commands;

public class CreateStoreCommand:IRequest<Result<CreateStoreCommandResponse>>
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

public sealed class CreateStoreCommandResponse
{
    public int StoreId { get; set; }
}

public sealed class UpdateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
{
    public UpdateStoreCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Mağaza adı boş olamaz.")
            .MaximumLength(50).WithMessage("İşletme adı 50 karakterden fazla olamaz.");

        RuleFor(x => x.TagIds)
            .NotEmpty().WithMessage("En az bir kategori seçilmeli");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Lutfen adres bilgisini giriniz.");

        RuleFor(x => x.StoreWorkingHours)
            .Must(workingDays => workingDays != null && workingDays.Count == 7)
            .WithMessage("Tüm haftanın çalışma saatleri belirtilmelidir (7 gün).");

        RuleForEach(x => x.StoreWorkingHours).ChildRules(w =>
        {
            w.RuleFor(x => x.DayOfWeek)
                .IsInEnum()
                .WithMessage("Geçersiz bir gün değeri girildi.");
            
            w.RuleFor(x => x.OpeningTime)
                .LessThan(x => x.ClosingTime)
                .When(x => !x.IsClosed && x.OpeningTime.HasValue && x.ClosingTime.HasValue)
                .WithMessage("Açılış saati kapanış saatinden önce olmalıdır.");
        });

    // RuleFor(x => x.CountOfStaff)
        //     .NotEmpty().WithMessage("Lütfen çalışan sayınızı belirtin.");
        //
        // RuleFor(x => x.OwnerName)
        //     .NotEmpty().WithMessage("İşletme sahibinin adı zorunludur.")
        //     .MaximumLength(100).WithMessage("İsim alanı 100 karakterden fazla olamaz.");
    }
}