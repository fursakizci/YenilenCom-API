using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Favourite:BaseEntity
{
    public int UserId { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public Date Date { get; set; }
    public string ImageUrl { get; set; }
}