namespace BDTheque.Model.Interfaces;

using BDTheque.Model.Attributes;

[InterfaceType]
[ApplyDefaultInterfaceMapping]
public interface IUniqueId
{
    public Guid Id { get; set; }
}
