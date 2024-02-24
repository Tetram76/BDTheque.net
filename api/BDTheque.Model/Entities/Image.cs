namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class Image : UniqueIdEntity
{
    public string? Titre { get; set; }

    [GraphQLIgnore]
    public byte[] Bytes { get; set; } = null!;

    public ushort Ordre { get; set; }

    public ushort TypeId { get; set; }
    public virtual Option Type { get; set; } = null!;

    public Guid EditionId { get; set; }
    public virtual EditionAlbum Edition { get; set; } = null!;
}
