using FluentValidation;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public sealed class GetAvailableSlutsForStaffQuery:IRequest<Result<List<GetAvailableSlutsForStaffQueryResponse>>>
{
    public int StaffId { get; set; } = 1;
    public int TotalServiceDuration { get; set; }
}

public sealed class GetAvailableSlutsForStaffQueryValidator : AbstractValidator<GetAvailableSlutsForStaffQuery>
{
    public GetAvailableSlutsForStaffQueryValidator()
    {
        RuleFor(c => c.StaffId)
            .NotEmpty().WithMessage("Staff secilmis olmali.");

        RuleFor(c => c.TotalServiceDuration)
            .NotEmpty().WithMessage("Hizmet süresi girilmelidir.")
            .GreaterThan(0).WithMessage("Hizmet süresi pozitif bir sayı olmalıdır.");
    }
}

public sealed class GetAvailableSlutsForStaffQueryResponse
{
    public DateTime Date { get; set; } // 2024-04-12
    public string DayName { get; set; } // sat
    public string Year { get; set; } //2025
    public string Month { get; set; } // april
    public string DayOfMonth { get; set; } // 12
    public List<TimeSlotDto> TimeSlots { get; set; }
}

