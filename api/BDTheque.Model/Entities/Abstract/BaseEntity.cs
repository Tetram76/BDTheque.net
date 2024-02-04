namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class BaseEntity : IVersioning
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
