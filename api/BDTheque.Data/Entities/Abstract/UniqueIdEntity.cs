namespace BDTheque.Data.Entities;

using BDTheque.Model.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    private static void SetupUniqueIdPrimaryKey<T>(EntityTypeBuilder<T> entity) where T : class, IUniqueIdEntity
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
    }
}
