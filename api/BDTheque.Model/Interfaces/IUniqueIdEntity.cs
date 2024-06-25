namespace BDTheque.Model.Interfaces;

[InterfaceType("IUniqueId")]
public interface IUniqueIdEntity : IEntity<Guid>
{
    // required for HotChocolate to add the interface to the GraphQL schema
    [ID]
    public new Guid Id { get; set; }
}
