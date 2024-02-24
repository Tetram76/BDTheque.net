namespace BDTheque.Data.Entities;

using System.Linq.Expressions;
using BDTheque.Data.Extensions;
using BDTheque.Extensions;
using BDTheque.Model.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    private static void SetupMandatoryInitiale<T, TProperty>(this EntityTypeBuilder<T> entity, Expression<Func<T, TProperty>> propertyExpression) where T : MandatoryLabelEntity
    {
        entity.Property(e => e.Initiale)
            .HasColumnType("character(1)")
            .HasMaxLength(1)
            .HasComputedColumnSql($"(upper({entity.GetColumnName(propertyExpression).ToSnakeCase()}))::character(1)", true);
    }
}
