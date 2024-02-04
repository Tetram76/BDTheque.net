namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class AssociableInterface<T> : UniqueIdInterface<T>
    where T : IAssociableEntity
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("Associable");
    }
}
