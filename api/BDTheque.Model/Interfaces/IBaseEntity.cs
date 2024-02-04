namespace BDTheque.Model.Interfaces;

internal interface IBaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
