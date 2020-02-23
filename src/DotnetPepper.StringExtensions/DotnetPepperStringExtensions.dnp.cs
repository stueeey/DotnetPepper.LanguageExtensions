using System.Collections.Generic;

namespace System
{
    internal static class DotnetPepperStringExtensions
    {
        internal static StringComparer DefaultComparer = StringComparer.CurrentCultureIgnoreCase;
        internal static bool IsNullOrWhiteSpace(this string value) => String.IsNullOrWhiteSpace(value);
        internal static bool EqualsIgnoringCase(this string value1, string value2) => DefaultComparer.Equals(value1, value2);
        internal static int CompareIgnoringCase(this string value1, string value2) => DefaultComparer.Compare(value1, value2);
        internal static string Join(this IEnumerable<string> value, string separator) => String.Join(separator, value);
    }
}
