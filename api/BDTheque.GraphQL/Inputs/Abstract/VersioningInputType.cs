namespace BDTheque.GraphQL.Inputs;

using System.Reflection;
using BDTheque.GraphQL.Extensions;
using BDTheque.Model.Entities.Abstract;

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
        IEnumerable<PropertyInfo> properties = typeof(T).GetProperties().Where(info => !InputObjectTypeDescriptorExtensions.IgnoredProperties.Any(func => func(info)));
        foreach (PropertyInfo property in properties)
            property.SetValue(toEntity, property.GetValue(fromEntity));
    }
}
