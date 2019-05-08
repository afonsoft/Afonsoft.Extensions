using System;

namespace Afonsoft.Extensions
{
    /// <summary>
    /// Classe para trabalhar com as Enum
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converter o valor de um Enum em um Enum
        /// </summary>
        /// <typeparam name="T">O enum de retorno</typeparam>
        /// <param name="value">Valor converter</param>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
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
        }
    }
}
