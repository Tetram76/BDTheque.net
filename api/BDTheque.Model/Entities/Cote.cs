namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class Cote : UniqueIdEntity
{
    public Guid EditionAlbumId { get; set; }
    public virtual EditionAlbum EditionAlbum { get; set; } = null!;

    [Year]
    public ushort Annee { get; set; }

    [NonNegative<decimal>]
    public decimal Prix { get; set; }
}
