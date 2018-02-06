using System;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Classe para trabalhar com a Regex
/// </summary>
public static class RegexExtensions
{

    /// <summary>
    /// Remover os caracteres especiais do texto
    /// </summary>
    public static string RemoveSpecialCharacters(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        value = Regex.Replace(value, "[«»\u201C\u201D\u201E\u201F\u2033\u2036]", "");
        value = Regex.Replace(value, "[èëêð]", "e");
        value = Regex.Replace(value, "[ÈËÊ]", "E");
        value = Regex.Replace(value, "[àâä]", "a");
        value = Regex.Replace(value, "[ÀÂÄÅ]", "A");
        value = Regex.Replace(value, "[ÙÛÜ]", "U");
        value = Regex.Replace(value, "[úûüµ]", "u");
        value = Regex.Replace(value, "[òöø]", "o");
        value = Regex.Replace(value, "[ÒÖØ]", "O");
        value = Regex.Replace(value, "[ìîï]", "i");
        value = Regex.Replace(value, "[ÌÎÏ]", "I");
        value = Regex.Replace(value, "[š]", "s");
        value = Regex.Replace(value, "[Š]", "S");
        value = Regex.Replace(value, "[ñ]", "n");
        value = Regex.Replace(value, "[Ñ]", "N");
        value = Regex.Replace(value, "[ÿ]", "y");
        value = Regex.Replace(value, "[Ÿ]", "Y");
        value = Regex.Replace(value, "[ž]", "z");
        value = Regex.Replace(value, "[Ž]", "Z");
        value = Regex.Replace(value, "[Ð]", "D");
        value = Regex.Replace(value, "[œ]", "oe");
        value = Regex.Replace(value, "[Œ]", "Oe");
        value = Regex.Replace(value, "[\"]", "");
        value = Regex.Replace(value, "[\t]", "");
        value = Regex.Replace(value, "[\n]", "");
        value = Regex.Replace(value, "[\r]", "");
        value = Regex.Replace(value, "[\u2026]", "...");
        value = Regex.Replace(value, Environment.NewLine, "");
        value = Regex.Replace(value, "��", "");
        value = Regex.Replace(value, "�", "");
        value = Regex.Replace(value, "[?]", "");
        return value.Trim();
    }


    private static bool invalid = false;
    public static bool IsValidEmail(this string strIn)
    {
        invalid = false;
        if (String.IsNullOrEmpty(strIn))
            return false;

        // Use IdnMapping class to convert Unicode domain names.
        try
        {
            strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper);
        }
        catch (Exception)
        {
            return false;
        }

        if (invalid)
            return false;

        // Return true if strIn is in valid e-mail format.
        try
        {
            // Return true if strIn is in valid e-mail format. 
            return Regex.IsMatch(strIn,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static string DomainMapper(Match match)
    {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            invalid = true;
        }
        return match.Groups[1].Value + domainName;
    }
}

