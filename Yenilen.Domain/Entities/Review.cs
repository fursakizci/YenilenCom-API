using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Review:BaseEntity
{
    public int AuthorId { get; set; }
    public User Author { get; set; }
    public string Text { get; set; }
    public decimal Rating { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public int? StaffId { get; set; }
    public Staff? Staff { get; set; }
    public bool IsVisible { get; set; } = true;
    public string? Reply { get; set; }
}