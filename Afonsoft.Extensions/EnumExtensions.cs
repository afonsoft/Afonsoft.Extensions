using System;
using System.Collections.Generic;
using System.Text;

public static class EnumExtensions
{
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static T ToEnum<T>(this string value, T defaultValue) where T : struct
    {
        if (string.IsNullOrEmpty(value))
        {
            return defaultValue;
        }

        T result = defaultValue;
        return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
    }
}
