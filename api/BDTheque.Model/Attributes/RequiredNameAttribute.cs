namespace BDTheque.Model.Attributes;

public class RequiredNameAttribute : RequiredAttribute
{
    public RequiredNameAttribute()
    {
        AllowEmptyStrings = false;
    }
}
