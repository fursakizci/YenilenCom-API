using FluentValidation;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public sealed class GetAvailableSlotsQuery:IRequest<Result<List<GetAvailableSlotsQueryResponse>>>
{
    public int StoreId { get; set; }
    //public DateTime StartingDate { get; set; }
    public int TotalServiceDuration { get; set; }
    public string TimeZoneId { get; set; } = "Turkey Standard Time";
}

public sealed class GetAvailableSlotsQueryValidator : AbstractValidator<GetAvailableSlotsQuery>
{
    public GetAvailableSlotsQueryValidator()
    {
        RuleFor(c => c.StoreId)
            .NotEmpty().WithMessage("Isletme secilmeli.");
        
        RuleFor(c => c.TotalServiceDuration)
            .NotEmpty().WithMessage("Hizmet süresi girilmelidir.")
            .GreaterThan(0).WithMessage("Hizmet süresi pozitif bir sayı olmalıdır.");

        RuleFor(c => c.TimeZoneId)
            .NotEmpty().WithMessage("Yerel saat dilimi girilmelidir. Örnek: Australia/Melbourne");
    }
}

public sealed class GetAvailableSlotsQueryResponse
{
    public DateTime Date { get; set; } // 2024-04-12
    public string DayName { get; set; } // sat
    public string Year { get; set; } //2025
    public string Month { get; set; } // april
    public string DayOfMonth { get; set; } // 12
    public List<TimeSlotDto> TimeSlots { get; set; }
}