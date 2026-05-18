namespace TaskManager.Domain.Entities.Base;

public class Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? ModifiedAt { get; private set; }
}