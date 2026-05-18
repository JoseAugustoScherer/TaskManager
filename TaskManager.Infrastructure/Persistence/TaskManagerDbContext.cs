using Microsoft.EntityFrameworkCore;

namespace TaskManager.Infrastructure.Persistence;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options){ }
    
    public DbSet<Task> Tasks { get; set; }
}