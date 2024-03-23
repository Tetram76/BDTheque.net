namespace BDTheque.GraphQL.Attributes;

using BDTheque.Model.Entities.Abstract;

[AttributeUsage(AttributeTargets.Class)]
public class MutationEntityAttribute<T> : Attribute where T : UniqueIdEntity
{
    public Type EntityType { get; set; } = typeof(T);
}
