using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace WordCount.Abstractions.SystemAbstractions.Reflection
{
    [ExcludeFromCodeCoverage]
    public class EntryAssembly : IAssembly
    {
        private readonly Assembly _entryAssembly;

        public EntryAssembly()
        {
            _entryAssembly = Assembly.GetEntryAssembly();
        }

        public string Name => _entryAssembly.GetName().Name;

        public string Version => _entryAssembly.GetName().Version.ToString();
    }
}
