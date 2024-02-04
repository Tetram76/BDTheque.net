namespace BDTheque.Model.Interfaces;

public interface IBaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
