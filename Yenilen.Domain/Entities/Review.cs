using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Review:BaseEntity
{
    public User Author { get; set; }
    public string Text { get; set; }
    public float Rating { get; set; }
    public Date Type { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public Staff Staff { get; set; }
    public bool IsVisible { get; set; } = true;
    public string? Reply { get; set; }
}