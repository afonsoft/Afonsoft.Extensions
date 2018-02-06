using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


/// <summary>
/// Classe com extenção para STRING 
/// AFONSO DUTRA NOGUEIRA FILHO
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Retorna os ultimos caracteres do texto ex. (123456789).GetLast(4) => 6789
    /// </summary>
    /// <param name="text"></param>
    /// <param name="length">Quantidade do retorno</param>
    /// <returns>(123456789).GetLast(4) => 6789</returns>
    public static string GetLast(this string text, int length)
    {
        if (length >= text.Length)
            return text;
        return text.Substring(text.Length - length);
    }

    /// <summary>
    /// Returns an enumerable collection of the specified type containing the substrings in this instance that are delimited by elements of a specified Char array
    /// </summary>
    /// <param name="text">The string.</param>
    /// <param name="separator">
    /// An array of Unicode characters that delimit the substrings in this instance, an empty array containing no delimiters, or null.
    /// </param>
    /// <typeparam name="T">
    /// The type of the elemnt to return in the collection, this type must implement IConvertible.
    /// </typeparam>
    /// <returns>
    /// An enumerable collection whose elements contain the substrings in this instance that are delimited by one or more characters in separator. 
    /// </returns>
    public static IEnumerable<T> SplitTo<T>(this string text, params char[] separator) where T : IConvertible
    {
        foreach (var s in text.Split(separator, StringSplitOptions.None))
            yield return (T)Convert.ChangeType(s, typeof(T));
    }

    /// <summary>
    /// Igual a função String.Format
    /// </summary>
    public static string Format(this string format, object arg, params object[] additionalArgs)
    {
        if (additionalArgs == null || additionalArgs.Length == 0)
        {
            return string.Format(format, arg);
        }
        else
        {
            return string.Format(format, new[] { arg }.Concat(additionalArgs).ToArray());
        }
    }

    /// <summary>
    /// Converter a string em uma "SecureString"
    /// </summary>
    /// <param name="text">Input String</param>
    public static System.Security.SecureString ToSecureString(this string text)
    {

        System.Security.SecureString secureString = new System.Security.SecureString();
        foreach (Char c in text)
            secureString.AppendChar(c);

        return secureString;
    }

    /// <summary>
    /// Verificar se é numerico o valor da string
    /// </summary>
    public static bool IsNumeric(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        long retNum;
        return long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out retNum);
    }

    /// <summary>
    /// ToUpperCaseFirst - Deixar somente a Primeira Letra do texto em maiusculo.
    /// <example>
    /// Ex: afonso dutra nogueira => Afonso dutra nogueira
    /// <code>
    /// string t = "afonso dutra nogueira";
    /// string r = t.ToUpperCaseFirst(); //Afonso dutra nogueira
    /// </code>
    /// </example>
    /// </summary>
    public static string ToUpperCaseFirst(this string text)
    {
        //https://www.dotnetperls.com/uppercase-first-letter
        if (string.IsNullOrWhiteSpace(text))
            return text;
        text = text.ToLower();
        char[] a = text.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

    /// <summary>
    /// ToUpperCaseWords - Deixar as primeiras Letra do texto em maiusculo.
    /// <example>
    /// afonso dutra nogueira => Afonso Dutra Nogueira
    /// <code>
    ///  string t = "afonso dutra nogueira";
    /// string r = t.ToUpperCaseFirst(); //Afonso Dutra Nogueira
    /// </code>
    /// </example>
    /// </summary>
    public static string ToUpperCaseWords(this string text)
    {
        //https://www.dotnetperls.com/uppercase-first-letter

        if (string.IsNullOrWhiteSpace(text))
            return text;
        text = text.ToLower();

        char[] array = text.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }

    /// <summary>
    /// Remover os acentos do texto
    /// </summary>
    public static string RemoveAccents(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        StringBuilder sbReturn = new StringBuilder();
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
        foreach (char letter in arrayText)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);
        }
        return sbReturn.ToString().Trim();
    }
  
    /// <summary>
    /// Validar se o conteudo da String é um CNPJ
    /// </summary>
    public static bool IsCnpj(this string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        if (cnpj.Length != 14)
            return false;
        var tempCnpj = cnpj.Substring(0, 12);
        var soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
        var resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        var digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cnpj.EndsWith(digito);
    }

    /// <summary>
    /// Validar se o conteudo da String é um CPF
    /// </summary>
    public static bool IsCpf(this string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        var resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        var digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }

}
