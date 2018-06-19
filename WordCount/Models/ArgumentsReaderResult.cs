namespace WordCount.Models
{
    public class ArgumentsReaderResult
    {
        public ArgumentsReaderResult(
            string sourceTextFile,
            bool isSourceTextFilePresent)
        {
            SourceTextFile = sourceTextFile;
            IsSourceTextFilePresent = isSourceTextFilePresent;
        }

        public string SourceTextFile { get; }

        public bool IsSourceTextFilePresent { get; }
    }
}
