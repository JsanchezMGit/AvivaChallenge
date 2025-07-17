namespace Payment.Enterprice.Shared;

public static class EnumExtensions
{
    public static TEnum? TryParse<TEnum>(this string value, bool ignoreCase = true) 
        where TEnum : struct, Enum
    {
        return Enum.TryParse(value, ignoreCase, out TEnum result) ? result : null;
    }

    public static bool IsDefined<TEnum>(this string value, bool ignoreCase = true)
        where TEnum : struct, Enum
    {
        return Enum.TryParse<TEnum>(value, ignoreCase, out _);
    }
}
