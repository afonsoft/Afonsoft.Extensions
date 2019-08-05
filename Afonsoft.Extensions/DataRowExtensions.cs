using System;
using System.Linq;

namespace Afonsoft.Extensions
{

    /// <summary>
    /// Classe para trabalhar com DataRow
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// Retorna o objeto da coluna selecionada
        /// </summary>
        public static T GetValue<T>(this System.Data.DataRow dr, String columnName)
        {
            if (dr == null)
                return default(T);

            if (String.IsNullOrEmpty(columnName))
                return default(T);

            if (!dr.Table.Columns.Contains(columnName))
                throw new ArgumentOutOfRangeException(columnName, "Esta coluna (" + columnName + ") não existe na tabela.");

            if (dr.IsNull(columnName))
                return default(T);

            int index = dr.Table.Columns.IndexOf(columnName);
            return (index < 0 || index > dr.ItemArray.Count())
                      ? default(T)
                      : (T)dr[index];
        }

        /// <summary>
        /// Retorna o objeto da coluna selecionada
        /// </summary>
        public static Object GetValue(this System.Data.DataRow dr, String columnName, Object defaultValue = null)
        {
            if (dr == null)
                return defaultValue;

            if (String.IsNullOrEmpty(columnName))
                return defaultValue;

            if (!dr.Table.Columns.Contains(columnName))
                throw new ArgumentOutOfRangeException(columnName, "Esta coluna (" + columnName + ") não existe na tabela.");

            if (dr.IsNull(columnName))
                return defaultValue;

            return dr[columnName];
        }

        /// <summary>
        /// Retorna o texto da coluna selecionada
        /// </summary>
        public static String GetString(this System.Data.DataRow dr, String columnName, String defaultValue = null)
        {
            try
            {
                String value = dr.GetValue<String>(columnName);

                if (string.IsNullOrEmpty(value))
                    return defaultValue;

                return value.Trim();
            }
            catch (Exception)
            {
                object value = dr.GetValue(columnName);
                if (value == null)
                    return defaultValue;

                return value.ToString().Trim();
            }
        }

        /// <summary>
        /// Retorna o double da coluna selecionada
        /// </summary>
        public static Double GetDouble(this System.Data.DataRow dr, String columnName)
        {
            try
            {
                return dr.GetValue<Double>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    String value = dr.GetValue<String>(columnName);
                    if (String.IsNullOrEmpty(value))
                        return 0;

                    value = value.Replace(",", ".");
                    return Convert.ToDouble(value);
                }
                catch (Exception)
                {
                    return Convert.ToDouble(dr.GetValue(columnName));
                }
            }
        }

        /// <summary>
        /// Retorna o double da coluna selecionada
        /// </summary>
        public static double? GetDouble(this System.Data.DataRow dr, String columnName, double? defaultValue = null)
        {
            try
            {
                return dr.GetValue<Double>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    String value = dr.GetValue<String>(columnName);
                    if (String.IsNullOrEmpty(value))
                        return defaultValue;

                    value = value.Replace(",", ".");
                    return Convert.ToDouble(value);
                }
                catch (Exception)
                {
                    try
                    {
                        return Convert.ToDouble(dr.GetValue(columnName));
                    }
                    catch (Exception)
                    {
                        return defaultValue;
                    }
                }
            }
        }

        /// <summary>
        /// Retorna o double da coluna selecionada
        /// </summary>
        public static Decimal GetDecimal(this System.Data.DataRow dr, String columnName)
        {
            try
            {
                return dr.GetValue<Decimal>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    String value = dr.GetValue<String>(columnName);
                    if (String.IsNullOrEmpty(value))
                        return 0;

                    value = value.Replace(",", ".");
                    return Convert.ToDecimal(value);
                }
                catch (Exception)
                {
                    return Convert.ToDecimal(dr.GetValue(columnName));
                }
            }
        }

        /// <summary>
        /// Retorna o campo DateTime da coluna selecionada
        /// </summary>
        public static DateTime GetDateTime(this System.Data.DataRow dr, String columnName)
        {
            try
            {
                return dr.GetValue<DateTime>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    return Convert.ToDateTime(dr.GetValue<String>(columnName));

                }
                catch (Exception)
                {
                    return (DateTime)dr.GetValue(columnName);
                }
            }
        }

        /// <summary>
        /// Retorna o campo DateTime da coluna selecionada
        /// </summary>
        public static DateTime? GetDateTime(this System.Data.DataRow dr, String columnName, DateTime? defaultValue = null)
        {
            try
            {
                return dr.GetValue<DateTime>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    DateTime? dt = dr.GetValue(columnName) as DateTime?;

                    if (dt == null)
                        return Convert.ToDateTime(dr.GetValue<String>(columnName));
                    else
                        return dt;

                }
                catch (Exception)
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Retorna o double da coluna selecionada
        /// </summary>
        public static Nullable<Decimal> GetDecimal(this System.Data.DataRow dr, String columnName, Nullable<Decimal> defaultValue = null)
        {
            try
            {
                return dr.GetValue<Decimal>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    String value = dr.GetValue<String>(columnName);
                    if (String.IsNullOrEmpty(value))
                        return defaultValue;

                    value = value.Replace(",", ".");
                    return Convert.ToDecimal(value);
                }
                catch (Exception)
                {
                    try
                    {
                        return Convert.ToDecimal(dr.GetValue(columnName));
                    }
                    catch (Exception)
                    {
                        return defaultValue;
                    }
                }
            }
        }

        /// <summary>
        /// Retorna o Boolean da coluna selecionada
        /// </summary>
        public static Boolean GetBoolean(this System.Data.DataRow dr, String columnName)
        {
            try
            {
                return dr.GetValue<Boolean>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    return Convert.ToBoolean(dr.GetValue(columnName));

                }
                catch (Exception)
                {
                    try
                    {
                        if (dr.GetValue<int>(columnName) == 1)
                            return true;
                        if (dr.GetValue<int>(columnName) == 0)
                            return false;

                        return false;

                    }
                    catch (Exception)
                    {
                        if (dr.GetValue<String>(columnName) == "1")
                            return true;
                        if (dr.GetValue<String>(columnName) == "0")
                            return false;

                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Retorna o Boolean da coluna selecionada
        /// </summary>
        public static Nullable<Boolean> GetBoolean(this System.Data.DataRow dr, String columnName, Nullable<Boolean> defaultValue = null)
        {
            try
            {
                return dr.GetValue<Boolean>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    return Convert.ToBoolean(dr.GetValue(columnName));

                }
                catch (Exception)
                {
                    try
                    {
                        if (dr.GetValue<int>(columnName) == 1)
                            return true;
                        if (dr.GetValue<int>(columnName) == 0)
                            return false;

                        if (dr.GetValue<String>(columnName) == "1")
                            return true;
                        if (dr.GetValue<String>(columnName) == "0")
                            return false;

                        return defaultValue;
                    }
                    catch (Exception)
                    {
                        return defaultValue;
                    }
                }
            }
        }

        /// <summary>
        /// Retorna o Integer da coluna selecionada
        /// </summary>
        public static Int32 GetInteger(this System.Data.DataRow dr, String columnName)
        {
            try
            {
                return dr.GetValue<Int32>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    return Convert.ToInt32(dr.GetValue<String>(columnName));
                }
                catch (Exception)
                {
                    return Convert.ToInt32(dr.GetValue(columnName));
                }
            }
        }

        /// <summary>
        /// Retorna o Integer da coluna selecionada
        /// </summary>
        public static Nullable<Int32> GetInteger(this System.Data.DataRow dr, String columnName, Nullable<Int32> defaultValue = null)
        {
            try
            {
                return dr.GetValue<Int32>(columnName);
            }
            catch (Exception)
            {
                try
                {
                    return Convert.ToInt32(dr.GetValue<String>(columnName));
                }
                catch (Exception)
                {
                    try
                    {
                        return Convert.ToInt32(dr.GetValue(columnName));
                    }
                    catch (Exception)
                    {
                        return defaultValue;
                    }
                }
            }
        }
    }
}