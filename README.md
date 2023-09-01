# StringHelper

This is a collection of small string helper functions.

## Functions

### DefaultIfNullOrEmpty

Returns the default value if the string is null or empty.

```csharp
result.DefaultIfNullOrEmpty("default value");
result.DefaultIfNullOrEmpty();                 // returns an empty string if result is null or empty
```

### DefaultIfNullOrWhiteSpace

Returns the default value if the string is null or white space only.

```csharp
result.DefaultIfNullOrWhiteSpace("default value");
result.DefaultIfNullOrWhiteSpace();            // returns an empty string if result is null or white space
```

### DefaultIfNull

Returns the default value if the string is null. If used without a default value, returns an empty string.

```csharp
result.DefaultIfNull("default value");
result.DefaultIfNull();                        // returns an empty string if result is null
```

### Deserialize

```csharp
public static T Deserialize<T>(this string s, JsonSerializerOptions? options = null)
```

Deserializes a string to an object using System.Text.Json.

```csharp
public record Person(string Name, int Age);
var result = "{\"Name\":\"John Doe\",\"Age\":42}".Deserialize<Person>();
```

### DeserializeCaseInsensitive

```csharp
public static T DeserializeCaseInsensitive<T>(this string s, JsonSerializerOptions? options = null)
```

Deserializes a string to an object using System.Text.Json. The property names are matched case insensitive.
The value of PropertyNameCaseInsensitive will always be set to true, even if options are specified and the
PropertyNameCaseInsensitive property is set to false.

```csharp
public record Person(string Name, int Age);
var result = "{\"name\":\"John Doe\",\"age\":42}".DeserializeCaseInsensitive<Person>();
```

### Serialize

```csharp
public static string Serialize<T>(this T obj, JsonSerializerOptions? options = null)
```

Serializes an object to a string using System.Text.Json.

```csharp
var result = new Person { Name = "John Doe", Age = 42 }.Serialize();
```

### ToBase64

```csharp
public static string ToBase64(this string s, Encoding? encoding = null)
```

Converts a string to a Base64 string. It uses encoding to encode the string to a byte array. If encoding is
null, UTF-8 is used.

```csharp
var result = "Hello World!".ToBase64(); // returns "SGVsbG8gV29ybGQh"
```

### FromBase64

```csharp
public static string FromBase64(this string s, Encoding? encoding = null)
```

Converts a Base64 string to a string. It uses encoding to decode the string from a byte array. If encoding is
null, UTF-8 is used.

```csharp
var result = "SGVsbG8gV29ybGQh".FromBase64(); // returns "Hello World!"
```

### RemovePrefix

```csharp
public static string RemovePrefix(this string s, string prefix, StringComparison comparisonType = StringComparison.Ordinal)
```

Removes a prefix from a string. If the prefix is not found, the original string is returned. The comparisonType
parameter specifies the comparison type. The default is Ordinal.


```csharp
var result = "Hello World!".RemovePrefix("Hello ");  // returns "World!"
```

### RemoveSuffix

```csharp
public static string RemoveSuffix(this string s, string suffix, StringComparison comparisonType = StringComparison.Ordinal)
```

Removes a suffix from a string. If the suffix is not found, the original string is returned. The comparisonType
parameter specifies the comparison type. The default is Ordinal.

```csharp
var result = "Hello World!".RemoveSuffix(" World!"); // returns "Hello"
```

### RemoveRegex

```csharp
public static string RemoveRegex(this string s, string pattern);
public static string RemoveRegex(this string s, Regex regex, RegexOptions? options = null);
```

Removes all occurrences of a regular expression from a string.

```csharp
var result = "Hello World!".RemoveRegex(@"[aeiou]"); // returns "Hll Wrld!"
var result = "Hello World!".RemoveRegex(new RegEx(@"[aeiou]")); // returns "Hll Wrld!"
```

### EndsWithNoCase

Determines whether the end of this string instance matches the specified string when compared using
StringComparison.OrdinalIgnoreCase. You could specify a StringComparison value, but this does not make
sense because you could just use the normal EndsWith method.

```csharp
var result = "Hello World!".EndsWithNoCase("world!"); // returns true
```
### StartsWithNoCase

Determines whether the beginning of this string instance matches the specified string when compared using
StringComparison.OrdinalIgnoreCase. You could specify a StringComparison value, but this does not make
sense because you could just use the normal StartsWith method.

```csharp
var result = "Hello World!".StartsWithNoCase("hello"); // returns true
```

### WithDirectorySeparator

Ensures that the string ends with a directory separator. If it already ends with a directory separator, the
original string is returned. This is true both for Path.DirectorySeparatorChar and Path.AltDirectorySeparatorChar.
If none of these characters are found at the end of the string, the Path.DirectorySeparatorChar is appended.

```csharp
var result = "C:\\Temp".WithDirectorySeparator(); // returns "C:\\Temp\\" on Windows
var result = "/tmp".WithDirectorySeparator();      // returns "/tmp/" on Linux and Mac OS
```

### WithoutDirectorySeparator

Ensures that the string does not end with a directory separator. If it already ends with a directory separator,
the string without the last character is returned. This is true both for Path.DirectorySeparatorChar and
Path.AltDirectorySeparatorChar.

```csharp
var result = "C:\\Temp\\".WithoutDirectorySeparator(); // returns "C:\\Temp" on Windows
var result = "/tmp/".WithoutDirectorySeparator();      // returns "/tmp" on Linux, Mac OS and Windows
```

### EndsWithDirectorySeparator

Determines whether the end of this string instance matches a directory separator. This is true both for
Path.DirectorySeparatorChar and Path.AltDirectorySeparatorChar.

```csharp
var result = "C:\\Temp\\".EndsWithDirectorySeparator(); // returns true on Windows
var result = "/tmp/".EndsWithDirectorySeparator();      // returns true on Linux, Mac OS and Windows
```

### ExpandEnvironmentVariables

Replaces the name of each environment variable embedded in the specified string with the string equivalent
of the value of the variable dictionary, then returns the resulting string. If `includeSystemVariables` is
true, the function will also replace system environment variables.

You can use this function to expand environment variables in a string without adding them to the current
process environment variables (for example to avoid problems with concurrent access to the environment).

You can also use it as a very simple template engine.

```csharp
var variables = new Dictionary<string, string?> { { "MYTMP", "C:\\Users\\JohnDoe\\AppData\\Local\\Temp" } };
var result = "%MYTMP%".ExpandEnvironmentVariables(variables); // returns "C:\\Users\\JohnDoe\\AppData\\Local\\Temp" on Windows
```

### Type check functions

These functions check whether a string contains a value of a specific type. They are useful if you want to
parse a string to a specific type but you are not sure if the string contains a valid value.

```csharp
s.IsInteger();    
s.IsLong();       
s.IsFloat();      
s.IsDouble();     
s.IsDecimal();    
s.IsBoolean(); 
s.IsDateTime();   
s.IsGuid();
s.IsTimeSpan();
```

### SanitizeFileName

```csharp
public static string SanitizeFileName(this string s, char replacement = '_')
```

Replaces all invalid characters in a file name with the specified replacement character. The default
replacement character is an underscore.

```csharp
var result = "Hello/World!".SanitizeFileName(); // returns "Hello_World!"
```


### Skip (deprecated since C# 8)

Skips the specified number of characters from the beginning of the string. This is similar to the Skip
function of the IEnumerable interface and should just be a little bit faster. Today you should use the
range operator instead like this: `var result = "Hello World!"[6..];`

```csharp
var result = "Hello World!".Skip(6); // returns "World!"
```

### Take (deprecated since C# 8)

Takes the specified number of characters from the beginning of the string. This is similar to the Take
function of the IEnumerable interface and should just be a little bit faster. Today you should use the
range operator instead like this: `var result = "Hello World!"[..5];`

```csharp
var result = "Hello World!".Take(5); // returns "Hello"
```

### ContainsNoCase

```csharp
public static bool ContainsNoCase(this string s, string value, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase);
```

This is just a wrapper for the Contains method. You could specify a StringComparison value, but this does not make
sense because you could just use the normal Contains method. It is useful if you want to use StringComparision.OrdinalIgnoreCase
but you don't want to write or read the long name.

```csharp
var result = "Hello World!".ContainsNoCase("world!"); // returns true
```

### ContainsAny

```csharp
public static bool ContainsAny(this string s, params string[] values);
public static bool ContainsAny(this string s, StringComparison comparisonType, params string[] values) 
public static bool ContainsAny(this string s, IEnumerable<string> values, StringComparison comparisonType = StringComparison.Ordinal) 
public static bool ContainsAnyNoCase(this string s, params string[] values)
```

Determines whether the string contains any of the specified values. The different overloads allow you to specify
a StringComparison value.

```csharp
var result = "Hello World!".ContainsAny("Hello", "Universe"); // returns true
```

### Repeat

```csharp
public static string Repeat(this string s, int count);
```

Returns a new string that repeats the specified string the specified number of times. If count is zero an empty
string is returned. If count is less than zero an ArgumentOutOfRangeException is thrown.

```csharp
var result = "Hello".Repeat(3); // returns "HelloHelloHello"
```

### IsAllAlphanumeric

```csharp
public static bool IsAllAlphanumeric(this string s);
```

Determines whether the string contains only alphanumeric characters.

```csharp
var result = "Hello World!".IsAllAlphanumeric(); // returns false
```

### IsAllAlphanumericOrUnderscore

```csharp
public static bool IsAllAlphanumericOrUnderscore(this string s);
```

Determines whether the string contains only alphanumeric characters or underscores.

```csharp
var result = "Hello World!".IsAllAlphanumericOrUnderscore(); // returns false
var result = "Hello_New_World".IsAllAlphanumericOrUnderscore(); // returns true
```

### IsAllNumeric

```csharp
public static bool IsAllNumeric(this string s);
```

Determines whether the string contains only numeric characters.

```csharp
var result = "123".IsAllNumeric(); // returns true
var result = "H123".IsAllNumeric(); // returns false
```

### EnsureLeft

```csharp
public static string EnsureLeft(this string s, string value, 
        StringComparison comparisonType = StringComparison.Ordinal);
```

Ensures that the string starts with the specified value. If the string already starts with the value, the
original string is returned. If the string does not start with the value, the value is prepended to the string.

_NOTE_: This function will always search for the full value. It will not search for a partial match.
For example "ello World!".EnsureLeft("Hello ") will return "Hello ello World!" and not "Hello World!".

```csharp
var result = "World!".EnsureLeft("Hello "); // returns "Hello World!"
var result = "Hello World!".EnsureLeft("Hello "); // returns "Hello World!"
```

### EnsureRight

```csharp
public static string EnsureRight(this string s, string value, 
        StringComparison comparisonType = StringComparison.Ordinal);
```

Ensures that the string ends with the specified value. If the string already ends with the value, the
original string is returned. If the string does not end with the value, the value is appended to the string.

```csharp
var result = "Hello".EnsureRight(" World!"); // returns "Hello World!"
var result = "Hello World!".EnsureRight(" World!"); // returns "Hello World!"
```

### HtmlEncode

```csharp
public static string HtmlEncode(this string s)
```

Encodes a string to be used in HTML.

```csharp
var result = "<Hello World!>".HtmlEncode(); // returns "&lt;Hello World!&gt;"
```

### HtmlDecode

```csharp
public static string HtmlDecode(this string s)
```

Decodes a string that has been encoded for HTML.

```csharp
var result = "&lt;Hello World!&gt;".HtmlDecode(); // returns "<Hello World!>"
```

### SurroundWith

```csharp
public static string SurroundWith(this string s, string valueLeft, string? valueRight = null)
```

Surrounds a string with the specified values. If valueRight is null, the valueLeft is used for both sides.

```csharp
var result = "Hello World!".SurroundWith("(", ")"); // returns "(Hello World!)"
var result = "Hello World!".SurroundWith("|"); // returns "|Hello World!|"
```


### TryParse

```csharp
public static bool TryParseInt(this string s, out int value) => int.TryParse(s, out value);
public static bool TryParseLong(this string s, out long value) => long.TryParse(s, out value);    
public static bool TryParseBoolean(this string s, out bool value) => bool.TryParse(s, out value);
public static bool TryParseGuid(this string s, out Guid value) => Guid.TryParse(s, out value);
```

These functions are wrapper for the TryParse functions of the different types. They are just sometimes more
convenient to use. Functions for other types are not included because I don't use them very often and they
are much more complicated to implement because of the dependency on IFormatProvider.

```csharp
if ("123".TryParseInt(out var value))
{
    // do something with value
}
```

### Break

```csharp
public static IEnumerable<string> Break(this string s, char ch)
```

Breaks the string at the first occurrence of the specified character and returns the part 
before and after the character. If the character is not found, the original string is returned
as the only element of the enumerable.

```csharp
var (before, after) = "Hello World!".Break(' '); // before = "Hello", after = "World!"
```

### ExpandTabs

```csharp
public static string ExpandTabs(this string s, int tabSize = 8)
```

Expands tabs to spaces. The default tab size is 8. If you want to use a different tab size, you can specify
it with the tabSize parameter. If you specify a tab size of 0, the function will remove all tabs. If you
specify a negative tab size, the function will return the original string.

```csharp
var result = "Hello\tWorld!".ExpandTabs(); // returns "Hello   World!" (3 spaces)
var result = "Hi\tWorld!".ExpandTabs(); // returns "Hi  World!" (2 spaces)
var result = "Hello\tWorld!".ExpandTabs(2); // returns "Hello World!" (1 space)
```

### CaseFold

```csharp
public static string CaseFold(this string s, bool useFullUnicode = true, bool useTurkic = false)
```

Case folding is like lower casing but more aggressive because it is intended to remove all case distinctions
present in a string. For example, the German lowercase letter 'ß' is equivalent to "ss". When case folding,
"ß" becomes "ss". Case folding is useful for comparing strings where the user input may contain characters
that do not have a case distinction.

The useFullUnicode parameter specifies whether the full Unicode case folding should be used. If it is false,
only the ASCII characters are case folded. The useTurkic parameter specifies whether the Turkic case folding
should be used. If it is false, the default case folding is used.

```csharp
var result = "Straße".CaseFold(); // returns "strasse"
```

### CollapseWhitespace

```csharp
public static string CollapseWhitespace(this string s)
```

Collapses all sequential whitespace characters to a single space.

```csharp
var result = "Hello   World!".CollapseWhitespace(); // returns "Hello World!"
var result = "Hello\nWorld!".CollapseWhitespace(); // returns "Hello World!"
```

### EditDistance

```csharp
public static int EditDistance(this string strings, string target)
```

Returns the edit distance (also called Levenshtein distance) between two strings. The edit distance is the
number of insertions, deletions, or substitutions required to transform one string into the other. The distance is
calculated using the Wagner–Fischer algorithm (see https://en.wikipedia.org/wiki/Wagner%E2%80%93Fischer_algorithm).

```csharp
var result = "Hello World!".EditDistance("Hello Universe!"); // returns 7
```

### Reverse

```csharp
public static string Reverse(this string s)
```

Reverses the string.

```csharp
var result = "Hello World!".Reverse(); // returns "!dlroW olleH"
```

### ParseFileSize

```csharp
public static long ParseFileSize(this string s)
```

Parses a file size string and returns the number of bytes. The input string is a number followed by an optional 
unit. The unit can be KB, MB, GB, TB or PB.

```csharp
var result = "1KB".ParseFileSize(); // returns 1024L
```


## String Enumerable Extensions

### CommonPrefix

```csharp
public static string CommonPrefix(this IEnumerable<string> strings)
```

Returns the common prefix of all strings in the enumerable. If the enumerable is empty, an
empty string is returned.

```csharp
var result = new[] { "Hello World!", "Hello Universe!" }.CommonPrefix(); // returns "Hello "
```

### CommonSuffix

```csharp
public static string CommonSuffix(this IEnumerable<string> strings)
```

Returns the common suffix of all strings in the enumerable. If the enumerable is empty, an
empty string is returned.

```csharp
var result = new[] { "Hello World!", "Hello Universe!" }.CommonSuffix(); // returns "!"
```
_Note_: This function is not very efficient because it reverses all strings.


### ToStream

```csharp
public static Stream ToStream(this IEnumerable<string> values,
        string separator = "\n",
        Encoding? encoding = null)
```

Converts an enumerable of strings to a stream. The stream contains all strings concatenated with the
specified separator. The default separator is a line feed. The default encoding is UTF-8.

```csharp
var seq = new[] {"Hello", "World"};
var stream = seq.ToStream();
var bytesRead = stream.Read(buffer, 0, 7); // reads "Hello\nW"
stream = seq.ToStream("")
bytesRead = stream.Read(buffer, 0, 7); // reads "HelloWo"
```

## StringBuilderExtensions

### AppendLineExpandEnvironment/AppendExpandEnvironment

```csharp
public static StringBuilder AppendExpandEnvironment(this StringBuilder sb, string s,
        IReadOnlyDictionary<string, string?>? env = null, bool includeSystem = true) 
public static StringBuilder AppendLineExpandEnvironment(this StringBuilder sb, string s,
        IReadOnlyDictionary<string, string?>? env = null, bool includeSystem = true)
```

These functions are similar to the ExpandEnvironmentVariables function from the string extensions, 
but they append the result to a StringBuilder.

```csharp
var sb = new StringBuilder();
Environment.SetEnvironmentVariable("MYTMP", "XYZ");
sb.AppendLineExpandEnvironment("%MYTMP%");
var result = sb.ToString(); // returns "XYZ" on Windows
```

The env parameter allows you to specify a dictionary with additional environment variables. This is useful if
you want to expand environment variables in a string without adding them to the current process environment for example
to avoid problems with concurrent access to the environment.

The includeSystem parameter allows you to specify whether the function should also replace system environment variables.