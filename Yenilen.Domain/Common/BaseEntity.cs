namespace Yenilen.Domain.Common;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteAt { get; set; }
}