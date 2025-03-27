using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Image:BaseEntity
{
    public string ImageUrl { get; set; }
    public int Index { get; set; }
}