using System.Data;
using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Features.Booking.Commands;

public sealed class CreateAppointmentCommand:IRequest<Result<CreateAppointmentResponse>>
{
    public int UserId { get; set; }
    public int StoreId { get; set; }
    public int StaffId { get; set; }
    public int CategoryId { get; set; }
    public List<int> ServicesId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan ServiceDuration { get; set; }
    public string AppointmentStatus { get; set; }
    public string Note { get; set; }
}

public sealed class CreateAppointmentResponse
{
    public int AppointmentId { get; set; }
}

public sealed class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(a => a.UserId)
            .NotEmpty().WithMessage("User bilgisi bos olamaz.");
        
        RuleFor(a => a.StoreId)
            .NotEmpty().WithMessage("Store bilgisi bos olamaz.");
        
        RuleFor(a => a.StaffId)
            .NotEmpty().WithMessage("Staff bilgisi bos olamaz.");
        
        RuleFor(a => a.ServicesId)
            .NotEmpty().WithMessage("Services bilgisi bos olamaz.");
        
        RuleFor(a => a.AppointmentDate)
            .NotEmpty().WithMessage("Randevu tarihi boş olamaz.")
            .Must(d => d.Kind == DateTimeKind.Utc)
            .WithMessage("Randevu tarihi UTC formatında olmalıdır.")
            .Must(d => d >= DateTime.UtcNow)
            .WithMessage("Randevu tarihi bugünden küçük olamaz.");
        
        RuleFor(a => a.ServiceDuration)
            .NotEmpty().WithMessage("Servis suresi bilgisi bos olamaz.");
    }
}