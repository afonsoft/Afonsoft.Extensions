using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Afonsoft.Extensions
{
    /// <summary>
    /// Classe interna para trabalhar com UFT8
    /// </summary>
    internal class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    /// <summary>
    /// Classe com extenção para OBJECT 
    /// AFONSO DUTRA NOGUEIRA FILHO
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Metodo para tratar o Null para Empty ou aspas
        /// </summary>
        /// <param name="Value"></param>
        /// <returns>Value ou Empty</returns>
        public static string NullToString(this object Value)
        {
            return Value == null ? "" : Value.ToString();
        }

        /// <summary>
        /// Serializar o Objeto - Criar o XML
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="toSerialize">Objeto a Serializar</param>
        /// <returns>XML</returns>
        public static string SerializeXml<T>(this T toSerialize)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
                using (StringWriter textWriter = new Utf8StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, toSerialize);
                    return textWriter.ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Serializar o Objeto - Criar o Json
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="toSerialize">Objeto a Serializar</param>
        /// <returns>JSON</returns>
        public static string SerializeJson<T>(this T toSerialize)
        {
            try
            {
                return JsonConvert
                    .SerializeObject(toSerialize, Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                    .Replace(Environment.NewLine, "")
                    .Replace("\n", "")
                    .Replace("\r", "");
            }
            catch
            {
                return JsonConvert.SerializeObject(toSerialize);
            }
        }

        /// <summary>
        /// Deserialize a string Json para um objeto
        /// </summary>
        /// <typeparam name="T">Tipo do objeto</typeparam>
        /// <param name="json">String em json</param>
        /// <returns>retorna o objeto preenchido</returns>
        public static T DeserializeJson<T>(this string json) where T : class
        {
            if (string.IsNullOrEmpty(json)) return default(T);
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        /// <summary>
        /// Converte o valor informado em um tipo desejado.
        /// </summary>
        /// <typeparam name="T">Tipo desejado.</typeparam>
        /// <param name="valor">Valor informado.</param>
        /// <param name="lancarExcecaoSeValorForNulo">Define se valores nulos devem retornar o valor padrao ou lancar excecao.</param>
        /// <returns>Valor convertido para o tipo desejado. Se o valor for nulo ou string vazia, retorna o valor default do tipo.</returns>
        public static T ParaTipo<T>(this object valor, bool lancarExcecaoSeValorForNulo = false)
        {
            if (!lancarExcecaoSeValorForNulo)
            {
                if (valor == null || valor == DBNull.Value || string.IsNullOrEmpty(valor.ToString()))
                {
                    return default(T);
                }
            }

            if (typeof(T).IsEnum)
            {
                return (T)valor;
            }

            return (T)Convert.ChangeType(valor, typeof(T));
        }
        /// <summary>
        /// Deserialize a string xml para um objeto
        /// </summary>
        /// <typeparam name="T">Tipo do objeto</typeparam>
        /// <param name="xml">String em XML</param>
        /// <returns>retorna o objeto preenchido</returns>
        public static T DeserializeXml<T>(this string xml) where T : class
        {
            if (string.IsNullOrEmpty(xml)) return default(T);
            try
            {
                StreamReader xmlStream = new StreamReader(xml);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}