using System.Collections.Specialized;

namespace WordCount.Abstractions.SystemAbstractions.Configuration
{
    public interface IConfigurationManager
    {
        NameValueCollection AppSettings { get; }
    }
}
