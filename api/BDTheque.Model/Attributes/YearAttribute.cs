namespace BDTheque.Model.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class YearAttribute() : RangeAttribute(1900, 2999);
