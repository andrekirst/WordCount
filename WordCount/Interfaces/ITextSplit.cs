using WordCount.Models.Results;

namespace WordCount.Interfaces
{
    public interface ITextSplit
    {
        TextSplitResult Split(string text);
    }
}
