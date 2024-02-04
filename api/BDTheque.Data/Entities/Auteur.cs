namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Auteur> entity)
    {
        entity.ToTable("Auteurs");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasOne(d => d.Personne).WithMany(p => p.Auteurs).HasForeignKey(d => d.PersonneId).OnDelete(DeleteBehavior.Restrict);
    }
}
