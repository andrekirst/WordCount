namespace WordCount.Abstractions.SystemAbstractions.Globalization
{
    public interface ICultureInfo
    {
        System.Globalization.CultureInfo GetCultureInfo(string culture);
    }
}