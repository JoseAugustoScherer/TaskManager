namespace TaskManager.Domain.Entities.Base;

public class SoftDeleteEntity : Entity
{
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; private set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}