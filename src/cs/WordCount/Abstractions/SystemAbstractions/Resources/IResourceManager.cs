namespace WordCount.Abstractions.SystemAbstractions.Resources
{
    public interface IResourceManager
    {
        string GetString(string name, System.Globalization.CultureInfo cultureInfo);
    }
}