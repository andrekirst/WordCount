namespace WordCount.Abstractions.ResourceManager
{
    public interface IResourceManager
    {
        string GetString(string name, System.Globalization.CultureInfo cultureInfo);
    }
}