namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    private static void SetupIdPrimaryKey<T>(EntityTypeBuilder<T> entity) where T : EntityWithId
    {
        entity.HasKey(e => e.Id);
    }
}
