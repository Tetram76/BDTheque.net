namespace BDTheque.Model.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MonthAttribute() : RangeAttribute(1, 12);
