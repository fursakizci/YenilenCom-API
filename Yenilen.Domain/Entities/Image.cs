using Yenilen.Domain.Common;
using Yenilen.Domain.Common.Enums;

namespace Yenilen.Domain.Entities;

public class Image:BaseEntity
{
    public int? StoreId { get; set; }
    public Store? Store { get; set; }
    public string ImageUrl { get; set; }
    public ImageType ImageType { get; set; }
}