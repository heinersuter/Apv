using System;

namespace Apv.Data.WindowsViewer.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsIgnoreCase(this string text, string pattern)
        {
            return text.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
