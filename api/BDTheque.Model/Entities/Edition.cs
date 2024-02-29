namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Scalars;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class Edition : UniqueIdEntity
{
    [Year]
    public ushort? AnneeEdition { get; set; }

    public string? Notes { get; set; }
    public string? NotesRaw { get; set; }

    [GraphQLType<IsbnType>]
    public string? Isbn { get; set; }

    public ushort? NombreDePages { get; set; }

    public bool? Couleur { get; set; } = true;
    public bool? Vo { get; set; } = false;

    public ushort? ReliureId { get; set; }

    [MutationType<OptionReliureType>]
    public virtual Option? Reliure { get; set; }

    public ushort? FormatEditionId { get; set; }

    [MutationType<OptionFormatEditionType>]
    public virtual Option? FormatEdition { get; set; }

    public ushort? TypeEditionId { get; set; }

    [MutationType<OptionTypeEditionType>]
    public virtual Option? TypeEdition { get; set; }

    public ushort? OrientationId { get; set; }

    [MutationType<OptionOrientationType>]
    public virtual Option? Orientation { get; set; }

    public ushort? SensLectureId { get; set; }

    [MutationType<OptionSensLectureType>]
    public virtual Option? SensLecture { get; set; }

    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();

    public virtual Serie? Serie { get; set; }
}
