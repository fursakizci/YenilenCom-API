using FluentValidation;
using MediatR;

namespace Yenilen.Application.Features.Service.Commands;

public sealed class CreateServiceCommand: IRequest<int>
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string CurrencyType { get; set; }
    public int DurationInMinutes { get; set; }
    public TimeSpan Duration => TimeSpan.FromMinutes(DurationInMinutes);
}

public sealed class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(c => c.CategoryId)
            .NotEmpty().WithMessage("Category secilmeli.");
        
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Isimlendirme alani bos olamaz.")
            .MaximumLength(50).WithMessage("Servis adi en fazla 50 karakter olabilir.");

        RuleFor(c => c.Description)
            .MaximumLength(1000).WithMessage("Aciklama alani maksimum 1000 karakter olabilir.");

        RuleFor(c => c.Price)
            .NotEmpty().WithMessage("Fiyat alani bos olamaz.")
            .GreaterThan(0).WithMessage("Fiyat 0'dan buyuk olmalidir.")
            .PrecisionScale(10, 2, false).WithMessage("Fiyat en fazla 10 basamakli ve 2 ondalikli olmalidir.");

        RuleFor(c => c.CurrencyType)
            .NotEmpty().WithMessage("Para cinsi bos olamaz.");

        RuleFor(c => c.DurationInMinutes)
            .NotEmpty().WithMessage("Service suresini belirtin")
            .Must(duration => duration >= 5).WithMessage("Servis suresi en az 5 dakika olmalidir.")
            .Must(duration => duration % 5 == 0).WithMessage("Servis suresi 5 dakikanin kati olmalidir.");
    }
}