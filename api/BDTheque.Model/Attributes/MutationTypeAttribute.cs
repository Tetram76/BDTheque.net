namespace BDTheque.Model.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MutationTypeAttribute<T> : Attribute where T : ScalarType
{
    public Type ScalarType { get; set; } = typeof(T);
}
