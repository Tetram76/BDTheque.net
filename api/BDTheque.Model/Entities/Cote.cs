namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;

using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
[Index(nameof(EditionId), nameof(Annee), IsUnique = true)]
public class Cote : UniqueIdEntity
{
    public Guid EditionId { get; set; }
    public virtual Edition Edition { get; set; } = null!;

    [Year]
    public ushort Annee { get; set; }

    [NonNegative<decimal>]
    public decimal Prix { get; set; }
}
