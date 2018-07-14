using System.Configuration;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class AppSettingsReader : IAppSettingsReader
    {
        public string DefaultLanguage => ConfigurationManager.AppSettings["defaultLanguage"];
    }
}
