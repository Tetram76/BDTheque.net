namespace BDTheque.Model.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class RequiredNameAttribute : RequiredAttribute
{
    public RequiredNameAttribute()
    {
        AllowEmptyStrings = false;
    }
}
