// ReSharper disable once CheckNamespace
namespace System;

public static class ArrayExtensions
{
    public static bool Exists<T>(this T[] array, Predicate<T> match) =>
        Array.Exists(array, match);

    public static T? Find<T>(this T[] array, Predicate<T> match) =>
        Array.Find(array, match);
}
