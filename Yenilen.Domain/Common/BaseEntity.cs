namespace Yenilen.Domain.Common;

public class BaseEntity
{
    public BaseEntity()
    {
        Uuid = Guid.NewGuid();
        IsActive = true;
        IsDeleted = false;
    }
    public int Id { get; set; }
    public Guid Uuid { get; set; }

    #region Audit Log

    public Guid CreateUserId { get; set; } = default!;
    public Guid? UpdateUserId { get; set; }
    public Guid? DeleteUserId { get; set; }
    
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteAt { get; set; }

    #endregion
   
}