using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Review:BaseEntity
{
    public string Text { get; set; }
    public float Rating { get; set; }
    public Date Type { get; set; }
    public Store Store { get; set; }
    public Staff Staff { get; set; }
}