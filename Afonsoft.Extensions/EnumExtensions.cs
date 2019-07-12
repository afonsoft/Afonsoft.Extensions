using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Afonsoft.Extensions
{
    /// <summary>
    /// Classe para trabalhar com as Enum
    /// </summary>
    public static class EnumExtensions
    {/// <summary>
     /// http://stackoverflow.com/questions/8582344/does-c-sharp-have-isnullorempty-for-list-ienumerable
     /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }
            /* If this is a list, use the Count property for efficiency. 
             * The Count property is O(1) while IEnumerable.Count() is O(N). */
            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }
        /// <summary>
        /// Converter o valor de um Enum em um Enum
        /// </summary>
        /// <typeparam name="T">O enum de retorno</typeparam>
        /// <param name="value">Valor converter</param>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static string ToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
        /// <summary>
        /// Converter o valor de um Enum em um Enum
        /// </summary>
        /// <typeparam name="T">O enum de retorno</typeparam>
        /// <param name="value">Valor converter</param>
        /// <param name="defaultValue">O default value deste Enum</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return Enum.TryParse<T>(value, true, out var result) ? result : defaultValue;
        }a
    }
}
