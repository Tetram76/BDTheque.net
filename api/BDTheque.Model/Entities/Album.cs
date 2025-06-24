namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Scalars;

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

    public ushort? Tome { get; set; }

    public bool? Integrale { get; set; } = false;
    public ushort? TomeDebut { get; set; }
    public ushort? TomeFin { get; set; }

    [Month]
    public ushort? MoisParution { get; set; }

    [Year]
    public ushort? AnneeParution { get; set; }

    public Guid? SerieId { get; set; }
    public virtual Serie? Serie { get; set; }

    public ushort? NotationId { get; set; }

    [MutationScalarType<OptionNotationType>]
    public virtual Option? Notation { get; set; }

    public virtual ICollection<AuteurAlbum> AuteursAlbums { get; set; } = new List<AuteurAlbum>();
    public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();
    public virtual ICollection<GenreAlbum> GenresAlbums { get; set; } = new List<GenreAlbum>();
    public virtual ICollection<UniversAlbum> UniversAlbums { get; set; } = new List<UniversAlbum>();

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Serie is null && string.IsNullOrWhiteSpace(Titre))
            yield return new ValidationResult("Album title is required when not attached to a Serie");
    }
}
