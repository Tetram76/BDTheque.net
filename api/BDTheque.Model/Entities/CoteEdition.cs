namespace BDTheque.Model.Entities;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class CoteEdition : UniqueIdEntity
{
    public Guid EditionAlbumId { get; set; }
    public virtual EditionAlbum EditionAlbum { get; set; } = null!;

    [Range(1900, 2999)]
    public ushort Annee { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Prix { get; set; }
}
