namespace BDTheque.GraphQL.Types;

using BDTheque.Data.Entities;

public class BaseInterface<T> : InterfaceType<T>
    where T : BaseEntity
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("Versioning");
    }
}
