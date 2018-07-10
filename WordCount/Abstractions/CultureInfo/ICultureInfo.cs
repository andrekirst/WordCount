namespace WordCount.Abstractions.CultureInfo
{
    public interface ICultureInfo
    {
        System.Globalization.CultureInfo GetCultureInfo(string culture);
    }
}