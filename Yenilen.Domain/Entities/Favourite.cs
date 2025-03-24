using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Favourite:BaseEntity
{
    public User User { get; set; }
    public Store Store { get; set; }
    public Date Date { get; set; }
}