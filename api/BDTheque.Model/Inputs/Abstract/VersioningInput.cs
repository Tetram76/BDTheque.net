namespace BDTheque.Model.Inputs;

using System.Reflection;
using BDTheque.Model.Attributes;
using BDTheque.Model.Entities.Abstract;

[ApplyMutationInputMapping]
public abstract class VersioningInput
{
    public T BuildEntity<T>()
        where T : VersioningEntity, new()
    {
        var newEntity = new T();
        ApplyUpdate(newEntity);
        return newEntity;
    }

    public void ApplyUpdate<T>(T toEntity)
    {
        PropertyInfo[]? thisProperties = GetType().GetProperties();
    }
}
