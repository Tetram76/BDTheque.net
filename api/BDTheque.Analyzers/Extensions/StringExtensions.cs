// ReSharper disable once CheckNamespace
namespace System;

public static class StringExtensions
{
    public static string ToCamelCase(this string input) =>
        string.IsNullOrEmpty(input) ? input : Char.ToLowerInvariant(input[0]) + input[1..];
}
