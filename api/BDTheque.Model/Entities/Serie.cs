namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Scalars;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class Serie : OptionalLabelEntity
{
    public string? Titre { get; set; }
    public string? TitreRaw { get; set; }

    public string? Sujet { get; set; }
    public string? SujetRaw { get; set; }

    public string? Notes { get; set; }
    public string? NotesRaw { get; set; }

    [Url]
    public Uri? SiteWeb { get; set; }

    public Guid? EditeurId { get; set; }
    public virtual Editeur? Editeur { get; set; }

    public Guid? CollectionId { get; set; }
    public virtual Collection? Collection { get; set; }

    public Guid ModeleEditionId { get; set; }
    public virtual Edition ModeleEdition { get; set; } = null!;

    public ushort? NbAlbums { get; set; }

    public bool? Terminee { get; set; } = false;
    public bool? Complete { get; set; } = false;

    public bool? SuivreSorties { get; set; } = true;
    public bool? SuivreManquants { get; set; } = true;

    public ushort? NotationId { get; set; }

    [MutationType<OptionNotationType>]
    public virtual Option? Notation { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<AuteurSerie> AuteursSeries { get; set; } = new List<AuteurSerie>();
    public virtual ICollection<GenreSerie> GenresSeries { get; set; } = new List<GenreSerie>();
    public virtual ICollection<UniversSerie> UniversSeries { get; set; } = new List<UniversSerie>();
}
