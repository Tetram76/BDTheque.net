namespace BDTheque.Extensions;

public static class EnumExtensions
{
    public static bool In<T>(this T value, params T[] values) where T : Enum =>
        values.Contains(value);
}
