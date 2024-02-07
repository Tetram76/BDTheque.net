namespace BDTheque.Model.Entities;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class Album : OptionalLabelEntity
{
    public string? Titre { get; set; }
    public string? TitreRaw { get; set; }

    public string? Sujet { get; set; }
    public string? SujetRaw { get; set; }

    public string? Notes { get; set; }
    public string? NotesRaw { get; set; }

    public bool? HorsSerie { get; set; } = false;

    [Range(0, Int32.MaxValue)]
    public ushort? Tome { get; set; }

    public bool? Integrale { get; set; } = false;

    [Range(0, Int32.MaxValue)]
    public ushort? TomeDebut { get; set; }

    [Range(0, Int32.MaxValue)]
    public ushort? TomeFin { get; set; }

    [Range(1, 12)]
    public ushort? MoisParution { get; set; }

    [Range(1900, 2999)]
    public ushort? AnneeParution { get; set; }

    public Guid? SerieId { get; set; }
    public virtual Serie? Serie { get; set; }

    public ushort? NotationId { get; set; }
    public virtual Option? Notation { get; set; }

    public virtual ICollection<AuteurAlbum> AuteursAlbums { get; set; } = new List<AuteurAlbum>();
    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();
    public virtual ICollection<GenreAlbum> GenresAlbums { get; set; } = new List<GenreAlbum>();
    public virtual ICollection<UniversAlbum> UniversAlbums { get; set; } = new List<UniversAlbum>();
}
