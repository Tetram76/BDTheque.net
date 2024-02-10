namespace BDTheque.Model.Interfaces;

[InterfaceType]
public interface IVersioning
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
