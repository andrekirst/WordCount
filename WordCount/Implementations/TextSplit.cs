using WordCount.Interfaces;
using WordCount.Extensions;
using WordCount.Models.Results;

namespace WordCount.Implementations
{
    public class TextSplit : ITextSplit
    {
        public TextSplitResult Split(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return new TextSplitResult();
            }

            var words = text.SplitByRegex(@"((\b[^\s\d]+\b)((?<=\.\w).)?)");
            return new TextSplitResult(words);
        }
    }
}
