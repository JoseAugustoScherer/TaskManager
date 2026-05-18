namespace TaskManager.Domain.Entities.Base;

public class Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}