namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class EditionAlbum : UniqueIdEntity
{
    public Guid EditionId { get; set; }
    public virtual Edition Edition { get; set; } = null!;

    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; } = null!;

    public Guid EditeurId { get; set; }
    public virtual Editeur Editeur { get; set; } = null!;

    public Guid? CollectionId { get; set; }
    public virtual Collection? Collection { get; set; }

    public ushort? EtatId { get; set; }
    public virtual Option? Etat { get; set; }

    public bool Stock { get; set; } = true;
    public bool? Offert { get; set; } = false;
    public bool? Occasion { get; set; } = false;
    public bool? Gratuit { get; set; } = false;

    public DateOnly? DateAchat { get; set; }

    [NonNegative<decimal>]
    public decimal? Prix { get; set; }

    public bool? Dedicace { get; set; } = false;

    public string? NumeroPerso { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Cote> Cotes { get; set; } = new List<Cote>();
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
