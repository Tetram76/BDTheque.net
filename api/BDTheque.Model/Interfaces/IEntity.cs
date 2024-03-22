namespace BDTheque.Model.Interfaces;

public interface IEntity<TKey>
{
    [ID]
    public TKey Id { get; set; }
}
