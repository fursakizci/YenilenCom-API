using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Appointment:BaseEntity
{
    public User User { get; set; }
    public Store Type { get; set; }
    public DateTime DateTime { get; set; }
}