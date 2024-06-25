// ReSharper disable once CheckNamespace
namespace System;

using System.Text;

public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var stringBuilder = new StringBuilder();
        var previousCharWasUpper = false;

        foreach (char c in input)
            if (char.IsUpper(c))
            {
                // Si ce n'est pas le premier caractère et que le caractère précédent n'était pas déjà en majuscule
                if (stringBuilder.Length != 0 && !previousCharWasUpper)
                    stringBuilder.Append('_');
                stringBuilder.Append(char.ToLowerInvariant(c));
                previousCharWasUpper = true;
            }
            else
            {
                stringBuilder.Append(c);
                previousCharWasUpper = false;
            }

        return stringBuilder.ToString();
    }
}
