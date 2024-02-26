namespace BDTheque.Data.Entities;

using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Auteur> entity)
    {
        entity.ToTable("auteurs");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasAlternateKey(
            e => new
            {
                e.PersonneId,
                e.Metier
            }
        );

        entity.HasOne(d => d.Personne).WithMany(p => p.Auteurs).HasForeignKey(d => d.PersonneId).OnDelete(DeleteBehavior.Restrict);
    }
}
