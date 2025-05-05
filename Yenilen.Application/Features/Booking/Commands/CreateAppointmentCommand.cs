using MediatR;

namespace Yenilen.Application.Features.Booking.Commands;

public sealed class CreateAppointmentCommand:IRequest<int>
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