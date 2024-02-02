namespace BDTheque.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class EntityWithUniqueId : BaseEntity
{
    public Guid Id { get; set; }
}

public static partial class ModelBuilderExtensions
{
    private static void SetupUniqueIdPrimaryKey<T>(EntityTypeBuilder<T> entity) where T : EntityWithUniqueId
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
    }
}
