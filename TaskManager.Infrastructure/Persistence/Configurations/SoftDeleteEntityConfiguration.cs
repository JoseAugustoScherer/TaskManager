using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Infrastructure.Persistence.Configurations;

public class SoftDeleteEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : SoftDeleteEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}