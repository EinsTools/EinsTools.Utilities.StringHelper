namespace EinsTools.Utilities.StringHelper;

/// <summary>
/// Extensions for StringBuilder
/// </summary>
public static class StringBuilderExtensions {
    
    /// <summary>
    /// -
    /// </summary>
    public static StringBuilder AppendExpandEnvironment(this StringBuilder sb, string s,
        IReadOnlyDictionary<string, string?>? env = null, bool includeSystem = true) {
        var e = env ?? ImmutableDictionary<string, string?>.Empty;
        return sb.Append(s.ExpandEnvironmentVariables(e, includeSystem));
    }
    
    /// <summary>
    /// -
    /// </summary>
    public static StringBuilder AppendLineExpandEnvironment(this StringBuilder sb, string s,
        IReadOnlyDictionary<string, string?>? env = null, bool includeSystem = true) {
        var e = env ?? ImmutableDictionary<string, string?>.Empty;
        return sb.AppendLine(s.ExpandEnvironmentVariables(e, includeSystem));
    }
}