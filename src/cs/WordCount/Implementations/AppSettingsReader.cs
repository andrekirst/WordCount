using WordCount.Abstractions.SystemAbstractions.Configuration;
using WordCount.Interfaces;

namespace WordCount.Implementations;

// TODO Replace with Options pattern
public class AppSettingsReader(IConfigurationManager configurationManager) : IAppSettingsReader
{
    private IConfigurationManager ConfigurationManager { get; } = configurationManager;

    public string DefaultLanguage => ConfigurationManager.AppSettings["defaultLanguage"];
}
