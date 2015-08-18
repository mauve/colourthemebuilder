using System.Globalization;

namespace colourthemebuilder.Extensions
{
    public static class StringExtensions
    {
        public static bool CaseInsensitiveContains(this string self, string needle)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(self, needle, CompareOptions.OrdinalIgnoreCase) >= 0;
        }
    }
}
