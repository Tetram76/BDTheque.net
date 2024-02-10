namespace BDTheque.Model.Interfaces;

[InterfaceType]
public interface IUniqueId
{
    [ID]
    public Guid Id { get; set; }
}
