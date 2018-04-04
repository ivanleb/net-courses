using System;

public static class StringExtensions
{
    public static string SetFirstLetterToUpperCase(this string target)
    {
        return $"{target[0].ToString().ToUpper()}{target.Substring(1)}";
    }

    public static string ApplyBraces(this string target)
    {
        return "{" + target + "}";
    }

    public static string ApplySpaces(this string target)
    {
        return $" {target} ";
    }

    public static string AppendNumbers(this string target)
    {
        return $"{target} - 12345";
    }

    public static string IncludeCurrentYear(this string target)
    {
        return $"{target} : {DateTime.Now.Year}";

    }

    public static string AppendSmile(this string target)
    {
        return $":){target}";
    }
}
