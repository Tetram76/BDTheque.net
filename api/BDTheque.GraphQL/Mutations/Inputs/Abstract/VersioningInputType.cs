namespace BDTheque.GraphQL.Mutations.Inputs;

using System.Reflection;
using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Extensions;
using BDTheque.Model.Mapping;

public abstract class VersioningInputType<T> : InputObjectType<T>
    where T : VersioningEntity
{
    protected override void Configure(IInputObjectTypeDescriptor<T> descriptor)
    {
        base.Configure(descriptor);

        descriptor.SetupDefaults();
    }

    public static void ApplyUpdate(T fromEntity, T toEntity)
    {
        IEnumerable<PropertyInfo> properties = typeof(T).GetProperties().Where(info => !MappingDefinitions.MutationInputIgnoredProperties.Any(func => func(info)));
        foreach (PropertyInfo property in properties)
            property.SetValue(toEntity, property.GetValue(fromEntity));
    }
}
