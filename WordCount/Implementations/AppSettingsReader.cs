using WordCount.Abstractions.SystemAbstractions.Configuration;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class AppSettingsReader : IAppSettingsReader
    {
        private readonly IConfigurationManager _configurationManager;

        public AppSettingsReader(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public string DefaultLanguage => _configurationManager.AppSettings["defaultLanguage"];
    }
}
