using System.Net;


namespace EinsTools.Utilities.StringHelper;

/// <summary>
/// Collection of utility functions for strings
/// </summary>
public static class StringExtensions {
    
    /// <summary>
    /// Returns the default value if the string is null or empty.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="defaultValue">Default value to use if the string is null or empty</param>
    /// <returns></returns>
    public static string DefaultIfNullOrEmpty(this string? s, string defaultValue) => 
        // Looks like a bug in dotnet standard, it doesn't recognize that s is not null after the null check
        s == null || string.IsNullOrEmpty(s) ? defaultValue : s;

    /// <summary>
    /// Returns the default value if the string is null or white space only.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="defaultValue">Default value to use if the string is null or empty</param>
    /// <returns></returns>
    public static string DefaultIfNullOrWhiteSpace(this string? s, string defaultValue = "") => 
        s == null || string.IsNullOrWhiteSpace(s) ? defaultValue : s;

    /// <summary>
    /// Returns the default value if the string is null. If used without a default value, returns an empty string.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="defaultValue">Default value to use if the string is null or empty</param>
    /// <returns></returns>
    public static string DefaultIfNull(this string? s, string defaultValue = "") => s ?? defaultValue;

    /// <summary>
    /// Deserializes a string to an object using System.Text.Json.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="options">JSON options to use</param>
    /// <typeparam name="T">Type to convert to</typeparam>
    /// <returns>Deserialized object</returns>
    public static T Deserialize<T>(this string s, JsonSerializerOptions? options = null) => 
        JsonSerializer.Deserialize<T>(s, options)!;
    
    /// <summary>
    /// Deserializes a string to an object using System.Text.Json. The property names are matched case insensitive.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="options">JSON options to use. The value of PropertyNameCaseInsensitive will
    /// always be set to true.</param>
    /// <typeparam name="T">Type to convert to</typeparam>
    /// <returns>Deserialized object</returns>
    public static T DeserializeCaseInsensitive<T>(this string s, JsonSerializerOptions? options = null) {
        var opts = options ?? new JsonSerializerOptions();
        opts.PropertyNameCaseInsensitive = true;
        return JsonSerializer.Deserialize<T>(s, opts)!;
    }
    
    /// <summary>
    /// Serializes an object to a string using System.Text.Json.
    /// </summary>
    /// <param name="obj">Object to serialize</param>
    /// <param name="options">JSON options</param>
    public static string Serialize<T>(this T obj, JsonSerializerOptions? options = null) => 
        JsonSerializer.Serialize(obj, options);

    /// <summary>
    /// Converts a string to a Base64 string.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="encoding">Encoding to use. Default encoding is UTF8.</param>s
    public static string ToBase64(this string s, Encoding? encoding = null) => 
        Convert.ToBase64String((encoding ?? Encoding.UTF8).GetBytes(s));

    /// <summary>
    /// Converts a Base64 string to a string.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="encoding">Encoding to use. Default encoding is UTF8.</param>
    /// <returns></returns>
    public static string FromBase64(this string s, Encoding? encoding = null) => 
        (encoding ?? Encoding.UTF8).GetString(Convert.FromBase64String(s));

    /// <summary>
    /// Removes the specified prefix from the string if it exists.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="prefix">Prefix to remove</param>
    /// <param name="comparisonType">Comparision type to use</param>
    /// <returns>String without prefix</returns>
    public static string RemovePrefix(this string s, string prefix, StringComparison comparisonType = StringComparison.Ordinal) => 
        s.StartsWith(prefix, comparisonType) ? s.Substring(prefix.Length) : s;

    /// <summary>
    /// Removes a suffix from a string. If the suffix is not found, the original string is returned. The comparisonType
    /// parameter specifies the comparison type. The default is Ordinal.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="suffix">Suffix to remove</param>
    /// <param name="comparisonType">Comparision type to use</param>
    /// <returns>String without suffix</returns>
    public static string RemoveSuffix(this string s, string suffix, StringComparison comparisonType = StringComparison.Ordinal) => 
        s.EndsWith(suffix, comparisonType) ? s.Substring(0, s.Length - suffix.Length) : s;

    /// <summary>
    /// Removes all occurrences of a regular expression from a string.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="prefix">The regular expression to remove</param>
    public static string RemoveRegex(this string s, Regex prefix) => 
        prefix.IsMatch(s) ? prefix.Replace(s, "") : s;

    /// <summary>
    /// Removes all occurrences of a regular expression from a string.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="prefix">The regular expression to remove</param>
    /// <param name="options">Regex options to use fr the match</param>
    public static string RemoveRegex(this string s, string prefix, RegexOptions? options = null) => 
        s.RemoveRegex(new Regex(prefix, options ?? RegexOptions.None));

    /// <summary>
    /// Determines whether the end of this string instance matches the specified string when compared using
    /// StringComparison.OrdinalIgnoreCase.
    /// </summary>
    /// <param name="s">Input String</param>
    /// <param name="value">Value to check</param>
    /// <param name="comparisonType">Comparison Type to use</param>
    public static bool EndsWithNoCase(this string s, string value, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase) 
        => s.EndsWith(value, comparisonType);

    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified string when compared using
    /// StringComparison.OrdinalIgnoreCase.
    /// </summary>
    /// <param name="s">Input String</param>
    /// <param name="value">Value to check</param>
    /// <param name="comparisonType">Comparison Type to use</param>
    /// <returns></returns>
    public static bool StartsWithNoCase(this string s, string value, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase) 
        => s.StartsWith(value, comparisonType);

    /// <summary>
    /// Ensures that the string ends with a directory separator. If it already ends with a directory separator, the
    /// original string is returned. This is true both for Path.DirectorySeparatorChar and Path.AltDirectorySeparatorChar.
    /// If none of these characters are found at the end of the string, the Path.DirectorySeparatorChar is appended.
    /// </summary>
    /// <param name="s">Input String</param>
    public static string WithDirectorySeparator(this string s) {
        if (s == "") return Path.DirectorySeparatorChar.ToString();
        var lc = s[s.Length - 1];
        if (lc == Path.DirectorySeparatorChar || lc == Path.AltDirectorySeparatorChar) return s;
        return s + Path.DirectorySeparatorChar;
    }
    
    /// <summary>
    /// Determines whether the end of this string instance matches a directory separator. This is true both for
    /// Path.DirectorySeparatorChar and Path.AltDirectorySeparatorChar.
    /// </summary>
    /// <param name="s">Input String</param>
    public static bool EndsWithDirectorySeparator(this string s) {
        if (s == "") return false;
        var lc = s[s.Length - 1];
        return lc == Path.DirectorySeparatorChar || lc == Path.AltDirectorySeparatorChar;
    }
    
    /// <summary>
    /// Ensures that the string does not end with a directory separator. If it already ends with a directory separator,
    /// the string without the last character is returned. This is true both for Path.DirectorySeparatorChar and
    /// Path.AltDirectorySeparatorChar.
    /// </summary>
    /// <param name="s">Input string</param>
    public static string WithoutDirectorySeparator(this string s) {
        if (s == "") return "";
        var lc = s[s.Length - 1];
        if (lc == Path.DirectorySeparatorChar || lc == Path.AltDirectorySeparatorChar) 
            return s.Substring(0, s.Length - 1);
        return s;
    }
    
    /// <summary>
    /// Returns true if the string can be parsed as an integer, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsInteger(this string s) => int.TryParse(s, out _);
    
    /// <summary>
    /// Returns true if the string can be parsed as a long, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsLong(this string s) => long.TryParse(s, out _);
    
    /// <summary>
    /// Returns true if the string can be parsed as a float, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsFloat(this string s) => float.TryParse(s, out _);
    
    /// <summary>
    /// Returns true if the string can be parsed as a double, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsDouble(this string s) => double.TryParse(s, out _);
    
    /// <summary>
    /// Returns true if the string can be parsed as a decimal, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsDecimal(this string s) => decimal.TryParse(s, out _);
    
    /// <summary>
    /// Returns true if the string can be parsed as a boolean, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsBoolean(this string s) => bool.TryParse(s, out _);
    
    /// <summary>
    /// Returns true if the string can be parsed as a datetime, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsDateTime(this string s) => DateTime.TryParse(s, out _);
    
    /// <summary>
    /// Returns true if the string can be parsed as a GUID, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsGuid(this string s) => Guid.TryParse(s, out _);
    
    /// <summary>
    /// Returns true if the string can be parsed as a time span, false otherwise.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsTimeSpan(this string s) => TimeSpan.TryParse(s, out _);
    
    /// <summary>
    /// Replaces all invalid characters in a file name with the specified replacement character. The default
    /// replacement character is an underscore.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="replacement">Replacement character, default is '_'</param>
    /// <returns></returns>
    public static string SanitizeFileName(this string s, char replacement = '_') {
        var invalidChars = Path.GetInvalidFileNameChars();
        var sb = new StringBuilder();
        foreach (var ch in s) {
            sb.Append(invalidChars.Contains(ch) ? replacement : ch);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Remove the first count characters from the string
    /// </summary>
    [Obsolete("Use string[n..] instead")]
    public static string Skip(this string s, int count) => s.Substring(count);

    /// <summary>
    /// Remove the last count characters from the string
    /// </summary>
    /// <param name="s"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [Obsolete("Use string[..n] instead")]
    public static string Take(this string s, int count) => s.Substring(0, count);

    private static bool InternalContains(this string s, string value, StringComparison comparisonType) {
        if (s == null) throw new ArgumentNullException(nameof(s));
        if (value == null) throw new ArgumentNullException(nameof(value));
        return s.IndexOf(value, comparisonType) >= 0;
    }
    
    /// <summary>
    /// This is just a wrapper for the Contains method with the string comparison OrdinalIgnoreCase.
    /// </summary>
    public static bool ContainsNoCase(this string s, string value, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase) 
        => s.InternalContains(value, comparisonType);
    
    /// <summary>
    /// Determines whether the string contains any of the specified values.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="values">Values to search</param>
    /// <returns></returns>
    public static bool ContainsAny(this string s, params string[] values) 
        => values.Any(s.Contains);

    /// <summary>
    /// Determines whether the string contains any of the specified values.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="comparisonType">Comparision type to use</param>
    /// <param name="values">Values to search</param>
    /// <returns></returns>
    public static bool ContainsAny(this string s, StringComparison comparisonType, params string[] values) 
        => values.Any(v => s.InternalContains(v, comparisonType));
    
    /// <summary>
    /// Determines whether the string contains any of the specified values.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="comparisonType">Comparision type to use</param>
    /// <param name="values">Values to search</param>
    public static bool ContainsAny(this string s, IEnumerable<string> values, StringComparison comparisonType = StringComparison.Ordinal) 
        => values.Any(v => s.InternalContains(v, comparisonType));
    
    /// <summary>
    /// Determines whether the string contains any of the specified values. Uses CompareNoCase for comparison.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="values">Values to search</param>
    /// <returns></returns>
    public static bool ContainsAnyNoCase(this string s, params string[] values)
        => values.Any(v => s.ContainsNoCase(v));
    
    /// <summary>
    /// Replaces the name of each environment variable embedded in the specified string with the string equivalent
    /// of the value of the variable dictionary, then returns the resulting string. If `includeSystemVariables` is
    /// true, the function will also replace system environment variables.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="variables">Variables to replace</param>
    /// <param name="includeSystemVariables">If true, environment variables will als be replaced.
    /// Defaults to true.</param>
    /// <returns></returns>
    public static string ExpandEnvironmentVariables(this string s, IReadOnlyDictionary<string, string?> variables,
        bool includeSystemVariables = true) {
        var inVar = false;
        var sb = new StringBuilder();
        var varName = new StringBuilder();
        foreach (var ch in s) {
            switch (ch) {
                case '%':
                    if (inVar) {
                        if (varName.Length == 0) {
                            sb.Append("%%");
                        }
                        else {
                            sb.Append(EvaluateVariable(varName.ToString(), variables, includeSystemVariables));
                            varName.Clear();
                        }
                        inVar = false;
                    }
                    else {
                        inVar = true;
                    }
                    break;
                default:
                    if (inVar) {
                        varName.Append(ch);
                    }
                    else {
                        sb.Append(ch);
                    }
                    break;
            }
        }

        if (inVar) {
            sb.Append("%" + varName);
        }
        return sb.ToString();
    }

    private static string EvaluateVariable(string varName, IReadOnlyDictionary<string, string?> variables, bool includeSystemVariables) {
        if (variables.TryGetValue(varName, out var value)) {
            return value ?? "";
        }
        
        if (includeSystemVariables) {
            return Environment.GetEnvironmentVariable(varName) ?? $"%{varName}%";
        }

        return $"%{varName}%";
    }
    /// <summary>
    /// Returns a new string that repeats the specified string the specified number of times. If count is zero an empty
    /// string is returned. If count is less than zero an ArgumentOutOfRangeException is thrown.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="count">Number of repetitions</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string Repeat(this string s, int count) {
        switch (count)
        {
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(count));
            case 0:
                return "";
            case 1:
                return s;
        }

        var sb = new StringBuilder(s.Length * count);
        for (var i = 0; i < count; i++) {
            sb.Append(s);
        }
        return sb.ToString();
    }
    
    /// <summary>
    /// Determines whether the string contains only alphanumeric characters.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsAllAlphanumeric(this string s) {
        return s.All(char.IsLetterOrDigit);
    }
    
    /// <summary>
    /// Determines whether the string contains only alphanumeric characters or underscores.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsAllAlphanumericOrUnderscore(this string s) {
        return s.All(c => char.IsLetterOrDigit(c) || c == '_');
    }
    
    /// <summary>
    /// Determines whether the string contains only numeric characters.
    /// </summary>
    /// <param name="s">Input string</param>
    public static bool IsAllNumeric(this string s) {
        return s.All(char.IsDigit);
    }
    
    /// <summary>
    /// Ensures that the string starts with the specified value. If the string already starts with the value, the
    /// original string is returned. If the string does not start with the value, the value is prepended to the string.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="value"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static string EnsureLeft(this string s, string value, 
        StringComparison comparisonType = StringComparison.Ordinal) {
        if (s.StartsWith(value, comparisonType)) return s;
        return value + s;
    }
    
    /// <summary>
    /// Ensures that the string ends with the specified value. If the string already ends with the value, the
    /// original string is returned. If the string does not end with the value, the value is appended to the string.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="value"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static string EnsureRight(this string s, string value, 
        StringComparison comparisonType = StringComparison.Ordinal) {
        if (s.EndsWith(value, comparisonType)) return s;
        return s + value;
    }
    
    /// <summary>
    /// Encodes a string to be used in HTML.
    /// </summary>
    /// <param name="s">Input string</param>
    public static string HtmlEncode(this string s) => WebUtility.HtmlEncode(s);
    
    /// <summary>
    /// Decodes a string that has been encoded for HTML.
    /// </summary>
    /// <param name="s">Input string</param>
    public static string HtmlDecode(this string s) => WebUtility.HtmlDecode(s);
    
    /// <summary>
    /// Surrounds a string with the specified values. If valueRight is null, the valueLeft is used for both sides.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="valueLeft">Value added to the left</param>
    /// <param name="valueRight">Value added to the right</param>
    /// <returns></returns>
    public static string SurroundWith(this string s, string valueLeft, string? valueRight = null) => 
        valueLeft + s + (valueRight ?? valueLeft);

    /// <summary>
    /// Wrapper for the TryParse functions of int
    /// </summary>
    public static bool TryParseInt(this string s, out int value) => int.TryParse(s, out value);
    /// <summary>
    /// Wrapper for the TryParse functions of long
    /// </summary>
    public static bool TryParseLong(this string s, out long value) => long.TryParse(s, out value);    
    /// <summary>
    /// Wrapper for the TryParse functions of bool
    /// </summary>
    public static bool TryParseBoolean(this string s, out bool value) => bool.TryParse(s, out value);
    /// <summary>
    /// Wrapper for the TryParse functions of Guid
    /// </summary>
    public static bool TryParseGuid(this string s, out Guid value) => Guid.TryParse(s, out value);

    /// <summary>
    /// Breaks the string at the first occurrence of the specified character and returns the part 
    /// before and after the character.
    /// </summary>
    public static (string before, string after) Break(this string s, char ch)
    {
        // Breaks the string at the first occurrence of the specified character
        // and returns the part before and after the character.
        var pos = s.IndexOf(ch);
        return pos < 0 
            ? (s, "") 
            : (s.Substring(0, pos), s.Substring(pos + 1));
    }
    
    
    /// <summary>
    /// Expands tabs to spaces. The default tab size is 8.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <param name="tabSize">Tab size, default is 8.</param>
    /// <returns></returns>
    public static string ExpandTabs(this string s, int tabSize = 8)
    {
        // Expands tabs in the string to spaces. The tab size is specified in
        // number of characters (default is 8).
        var sb = new StringBuilder();
        var col = 0;
        foreach (var ch in s)
        {
            if (ch == '\t')
            {
                var spaces = tabSize - col % tabSize;
                sb.Append(' ', spaces);
                col += spaces;
            }
            else
            {
                sb.Append(ch);
                col++;
                if (ch == '\n') col = 0;
            }
        }
        return sb.ToString();
    }
    
    /// <summary>
    /// Case folding is like lower casing but more aggressive because it is intended to remove all case distinctions
    /// present in a string.
    /// The useFullUnicode parameter specifies whether the full Unicode case folding should be used. If it is false,
    /// only the ASCII characters are case folded. The useTurkic parameter specifies whether the Turkic case folding
    /// should be used. If it is false, the default case folding is used.
    /// </summary>
    public static string CaseFold(this string s, bool useFullUnicode = true, bool useTurkic = false)
    {
        var sb = new StringBuilder();        
        foreach (var ch in s)
        {
            var uc = char.ConvertToUtf32(ch.ToString(), 0);
            if (CharUnicodeInfo.CaseFolding.TryGetValue(uc, out var fc))
            {
                // Replace the character with its folded equivalent.
                switch (fc.Category)
                {
                    case "C":
                    case "S":
                    case "F" when useFullUnicode:
                    case "T" when useTurkic:    
                        foreach (var i in fc.Value)
                        {
                            var x = char.ConvertFromUtf32(i);
                            sb.Append(x);
                        }
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            else
            {
                // No folding, just copy the character.
                sb.Append(ch);
            }
        }
        return sb.ToString();
    }
    
    /// <summary>
    /// Collapses all sequential whitespace characters to a single space.
    /// </summary>
    /// <param name="s">Input string</param>
    public static string CollapseWhitespace(this string s) {
        var sb = new StringBuilder();
        var inWhitespace = false;
        foreach (var ch in s) {
            if (char.IsWhiteSpace(ch)) {
                if (inWhitespace) continue;
                sb.Append(' ');
                inWhitespace = true;
            }
            else {
                sb.Append(ch);
                inWhitespace = false;
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Returns the edit distance between two strings. The edit distance is the
    /// number of insertions, deletions, or substitutions required to transform one string into the other.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static int EditDistance(this string s, string other) {
        // Calculate Levenshtein distance using Wagner-Fischer algorithm
        // https://en.wikipedia.org/wiki/Wagner%E2%80%93Fischer_algorithm
        var m = s.Length;
        var n = other.Length;
        var d = new int[m + 1, n + 1];
        for (var i = 0; i <= m; i++) {
            d[i, 0] = i;
        }
        for (var j = 0; j <= n; j++) {
            d[0, j] = j;
        }
        for (var j = 1; j <= n; j++) {
            for (var i = 1; i <= m; i++) {
                if (s[i - 1] == other[j - 1]) {
                    d[i, j] = d[i - 1, j - 1];
                }
                else {
                    d[i, j] = Math.Min(Math.Min(
                            d[i - 1, j] + 1, // deletion
                            d[i, j - 1] + 1), // insertion
                        d[i - 1, j - 1] + 1); // substitution
                }
            }
        }
        return d[m, n];
    }
    
    /// <summary>
    /// Reverses the string.
    /// </summary>
    /// <param name="s">Input string</param>
    public static string Reverse(this string s) {
        var sb = new StringBuilder(s.Length);
        for (var i = s.Length - 1; i >= 0; i--) {
            sb.Append(s[i]);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Parses a file size string and returns the number of bytes.
    /// The input string is a number followed by an optional unit.
    /// The unit can be KB, MB, GB, TB or PB
    /// </summary>
    /// <param name="str">The string to parse</param>
    /// <returns>The size as a long vaue</returns>
    public static long ParseFileSize(this string str)
    {
        var match = Regex.Match(str, @"^\s*(\d+)\s*([KMGTP]B)?\s*$", RegexOptions.IgnoreCase);
        if (!match.Success) throw new FormatException("Invalid file size format");
        var size = long.Parse(match.Groups[1].Value);
        var unit = match.Groups[2].Value.ToUpper();
        switch (unit)
        {
            case "KB":
                size *= 1024L;
                break;
            case "MB":
                size *= 1024L * 1024L;
                break;
            case "GB":
                size *= 1024L * 1024L * 1024L;
                break;
            case "TB":
                size *= 1024L * 1024L * 1024L * 1024L;
                break;
            case "PB":
                size *= 1024L * 1024L * 1024L * 1024L * 1024L;
                break;
            case "":
                break;
            default:
                throw new FormatException("Invalid file size format");
        }
        return size;
    }
    
}