using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Entities;

public class TaskItem : Entity
{
    public TaskStatus Status { get; private set; } = TaskStatus.Created;
    public string Title { get; private set; }
    public string Description { get; private set; }
    public User Owner { get; private set; }
    
    // EF Ctor
    private TaskItem(){}
    
    // Primary Constructor
    public TaskItem(TaskStatus status, string title, string description, User owner)
        => (Status, Title, Description, Owner) = (status, title, description, owner);
    
    // Factory
    public static TaskItem Create(string title, string description, User owner)
        => new (TaskStatus.Created, title, description, owner);
    
    // Class Methods
    public void UpodateStatus(TaskStatus newStatus) => Status = newStatus;
    public void UpdateTitle(string title) => Title = title;
    public void UpdateDescription(string description) => Description = description;
}