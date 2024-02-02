namespace BDTheque.Data.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class EntityWithId : BaseEntity
{
    public ushort Id { get; set; }
}

public static partial class ModelBuilderExtensions
{
    private static void SetupIdPrimaryKey<T>(EntityTypeBuilder<T> entity) where T : EntityWithId
    {
        entity.HasKey(e => e.Id);
    }
}
