namespace Yenilen.Application.DTOs;

public class AppointmentDto
{
    public int? UserId { get; set; }
    //public List<ServiceDto> Services { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }
    public string Note { get; set; }
}