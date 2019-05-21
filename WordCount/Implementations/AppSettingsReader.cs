using WordCount.Abstractions.SystemAbstractions.Configuration;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class AppSettingsReader : IAppSettingsReader
    {
        private IConfigurationManager ConfigurationManager { get; }

        public AppSettingsReader(IConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
        }

        public string DefaultLanguage => ConfigurationManager.AppSettings[name: "defaultLanguage"];
    }
}
