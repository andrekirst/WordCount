using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface ITextInput
    {
        string GetInputText(ArgumentsReaderResult argumentsReaderResult);
    }
}
