namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class AssociableInterface<T> : UniqueIdInterface<T>
    where T : AssociableEntity
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("Associable");
    }
}
