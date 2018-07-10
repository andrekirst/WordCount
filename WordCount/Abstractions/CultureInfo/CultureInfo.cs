using System.Diagnostics.CodeAnalysis;

namespace WordCount.Abstractions.CultureInfo
{
    [ExcludeFromCodeCoverage]
    public class CultureInfo : ICultureInfo
    {
        public System.Globalization.CultureInfo GetCultureInfo(string culture)
        {
            return System.Globalization.CultureInfo.GetCultureInfo(name: culture);
        }
    }
}