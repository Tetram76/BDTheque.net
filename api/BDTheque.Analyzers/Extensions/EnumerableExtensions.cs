// ReSharper disable once CheckNamespace
namespace System.Collections.Generic;

public static class EnumerableExtensions
{
    public static T? OnlyOrDefault<T>(this IEnumerable<T> source, Predicate<T> predicate)
    {
        T? result = default;
        bool found = false;

        foreach (T element in source.Where(arg => predicate(arg)))
        {
            if (found) return default;

            result = element;
            found = true;
        }

        return result;
    }
}
