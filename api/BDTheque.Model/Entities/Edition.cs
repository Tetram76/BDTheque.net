namespace BDTheque.Model.Entities;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Edition : EntityWithUniqueId
{
    [Range(0, 2999)]
    public ushort? AnneeEdition { get; set; }

    public string? Notes { get; set; }
    public string? NotesRaw { get; set; }

    public string? Isbn { get; set; }

    public ushort? NombreDePages { get; set; }

    public bool? Couleur { get; set; } = true;
    public bool? Vo { get; set; } = false;

    public ushort? ReliureId { get; set; }
    public virtual Option? Reliure { get; set; }

    public ushort? FormatEditionId { get; set; }
    public virtual Option? FormatEdition { get; set; }

    public ushort? TypeEditionId { get; set; }
    public virtual Option? TypeEdition { get; set; }

    public ushort? OrientationId { get; set; }
    public virtual Option? Orientation { get; set; }

    public ushort? SensLectureId { get; set; }
    public virtual Option? SensLecture { get; set; }

    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();

    public virtual Serie? Serie { get; set; }
}
