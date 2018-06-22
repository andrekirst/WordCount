namespace WordCount.Models
{
    public class ArgumentsReaderResult
    {
        public ArgumentsReaderResult(
            string sourceTextFile,
            bool isSourceTextFileParameterPresent,
            bool isIndexParameterPresent)
        {
            SourceTextFile = sourceTextFile;
            IsSourceTextFileParameterPresent = isSourceTextFileParameterPresent;
            IsIndexParameterPresent = isIndexParameterPresent;
        }

        public string SourceTextFile { get; }

        public bool IsSourceTextFileParameterPresent { get; }

        public bool IsIndexParameterPresent { get; }
    }
}
