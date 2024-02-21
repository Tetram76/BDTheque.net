namespace BDTheque.Model.Attributes;

using System.Reflection;

public class NonNegativeAttribute<T>() : RangeAttribute(typeof(T), "0", GetMaxValue().ToString()!)
    where T : IComparable
{
    private static T? _maxValue;

    private static T GetMaxValue()
    {
        if (_maxValue == null)
        {
            FieldInfo maxValueConst = typeof(T).GetField("MaxValue", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                      ?? throw new InvalidOperationException($"{typeof(T).Name} does not provide a MaxValue");
            if (!maxValueConst.FieldType.IsAssignableTo(typeof(T)))
                throw new InvalidOperationException($"{typeof(T).Name}.MaxValue is not of type {typeof(T).Name}");
            _maxValue = (T) maxValueConst.GetValue(null)!;
        }

        return _maxValue;
    }
}
