using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    /// <summary>
    /// Parses folder-related command-line arguments for multi-file analysis.
    /// </summary>
    /// <remarks>
    /// Supported arguments:
    /// <list type="bullet">
    /// <item><description>-f or --folder: Specifies the folder path to analyze</description></item>
    /// <item><description>-p or --pattern: Specifies the file pattern (default: "*.txt")</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// WordCount -f /path/to/documents -p *.md
    /// </example>
    public class FolderParameterParser : BaseParameterParser<FolderParameter>, IFolderParameterParser
    {
        private IEnvironment Environment { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderParameterParser"/> class.
        /// </summary>
        /// <param name="environment">The environment abstraction for accessing command-line arguments.</param>
        public FolderParameterParser(IEnvironment environment)
        {
            Environment = environment;
        }

        /// <inheritdoc />
        public FolderParameter ParseFolderParameter()
        {
            return CachedValue(() =>
            {
                var commandLineArgs = Environment.GetCommandLineArgs() ?? Array.Empty<string>();

                // Look for folder parameter with -f or --folder flag
                var folderIndex = Array.FindIndex(commandLineArgs,
                    arg => arg.Equals("-f", StringComparison.OrdinalIgnoreCase) ||
                           arg.Equals("--folder", StringComparison.OrdinalIgnoreCase));

                if (folderIndex == -1 || folderIndex >= commandLineArgs.Length - 1)
                {
                    return new FolderParameter
                    {
                        IsPresent = false,
                        FolderPath = string.Empty,
                        FilePattern = "*.txt"
                    };
                }

                var folderPath = commandLineArgs[folderIndex + 1];

                // Look for optional file pattern with -p or --pattern flag
                var patternIndex = Array.FindIndex(commandLineArgs,
                    arg => arg.Equals("-p", StringComparison.OrdinalIgnoreCase) ||
                           arg.Equals("--pattern", StringComparison.OrdinalIgnoreCase));

                var filePattern = "*.txt";
                if (patternIndex != -1 && patternIndex < commandLineArgs.Length - 1)
                {
                    filePattern = commandLineArgs[patternIndex + 1];
                }

                return new FolderParameter
                {
                    IsPresent = folderPath.IsFilled(),
                    FolderPath = folderPath,
                    FilePattern = filePattern
                };
            });
        }
    }
}
