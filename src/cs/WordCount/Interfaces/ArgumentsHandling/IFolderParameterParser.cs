using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    /// <summary>
    /// Defines the contract for parsing folder-related command-line parameters.
    /// </summary>
    /// <remarks>
    /// Parses the -f/--folder and -p/--pattern command-line arguments
    /// for multi-file analysis operations.
    /// </remarks>
    public interface IFolderParameterParser
    {
        /// <summary>
        /// Parses the folder parameter from command-line arguments.
        /// </summary>
        /// <returns>
        /// A <see cref="FolderParameter"/> containing the folder path and file pattern.
        /// If no folder argument is present, returns a parameter with IsPresent = false.
        /// </returns>
        FolderParameter ParseFolderParameter();
    }
}
