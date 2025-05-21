using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Favourite:BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
}