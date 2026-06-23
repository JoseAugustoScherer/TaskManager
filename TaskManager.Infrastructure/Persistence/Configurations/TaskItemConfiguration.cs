using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Title)
            .IsRequired();
        builder.Property(x => x.Status)
            .IsRequired();
        
        builder.HasOne(x =>x.Owner)
            .WithMany(u => u.Tasks)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}