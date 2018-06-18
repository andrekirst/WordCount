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

        public string SourceTextFile { get; private set; }

        public bool IsSourceTextFilePresent { get; private set; }
    }
}
