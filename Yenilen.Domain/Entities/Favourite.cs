using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Favourite:BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
}