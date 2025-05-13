using FluentValidation;
using MediatR;

namespace Yenilen.Application.Features.User.Commands;

public sealed class AddUserAddressCommand:IRequest<int>
{
    public string Label { get; set; }
    public string FullAddress { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string CountryCode { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string District { get; set; }
}

public sealed class AddUserAddressCommandValidator : AbstractValidator<AddUserAddressCommand>
{
    public AddUserAddressCommandValidator()
    {
        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("Etiket alanı boş olamaz.")
            .MaximumLength(30).WithMessage("Etiket alanının uzunlugu 30 karakterden fazla olamaz.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir alanı boş olamaz.");

        RuleFor(x => x.District)
            .NotEmpty().WithMessage("İlçe alanı boş olamaz.");

        RuleFor(x => x.Longitude)
            .NotEmpty().WithMessage("Boylam bilgisi boş olamaz.");

        RuleFor(x => x.Latitude)
            .NotEmpty().WithMessage("Enlem bilgisi boş olamaz.");
    }
}

