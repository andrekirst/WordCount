using System.Diagnostics.CodeAnalysis;

namespace WordCount.Abstractions.SystemAbstractions.Globalization
{
    [ExcludeFromCodeCoverage]
    public class CultureInfo : ICultureInfo
    {
        public System.Globalization.CultureInfo GetCultureInfo(string culture) => System.Globalization.CultureInfo.GetCultureInfo(name: culture);
    }
}