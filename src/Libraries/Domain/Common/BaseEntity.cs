namespace Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedTime { get; set; }
}