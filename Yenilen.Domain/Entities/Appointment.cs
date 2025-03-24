using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Appointment:BaseEntity
{
    public User User { get; set; }
    public Store Store { get; set; }
    public Category Category { get; set; }
    public Service Service { get; set; }
    public Date Date { get; set; }
    public int Duration { get; set; }
    public string Note { get; set; }
}