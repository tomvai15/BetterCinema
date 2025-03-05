namespace BetterCinema.Tools.App;

public static class StringExtensions
{
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        
        return string.Concat(str[..1].ToLower(), str.AsSpan(1));
    }
}