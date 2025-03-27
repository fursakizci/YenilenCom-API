using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Staff:BaseEntity
{
    public int StoreId { get; set; }
    public string Name { get; set; }
    public Image Image { get; set; }
}