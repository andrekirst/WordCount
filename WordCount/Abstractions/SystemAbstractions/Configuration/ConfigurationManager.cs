using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace WordCount.Abstractions.SystemAbstractions.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ConfigurationManager : IConfigurationManager
    {
        public NameValueCollection AppSettings => System.Configuration.ConfigurationManager.AppSettings;
    }
}
