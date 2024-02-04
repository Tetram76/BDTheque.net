namespace BDTheque.Model.Interfaces;

public interface IVersioning
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
