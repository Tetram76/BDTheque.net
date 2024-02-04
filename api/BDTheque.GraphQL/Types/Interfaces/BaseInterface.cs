namespace BDTheque.GraphQL.Types.Interfaces;

using BDTheque.Model.Interfaces;

public class BaseInterface<T> : InterfaceType<T>
    where T : IBaseEntity
{
    protected override void Configure(IInterfaceTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("Versioning");
    }
}
