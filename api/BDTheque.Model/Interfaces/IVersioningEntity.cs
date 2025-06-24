namespace BDTheque.Model.Interfaces;

[InterfaceType("IVersioning")]
public interface IVersioningEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
