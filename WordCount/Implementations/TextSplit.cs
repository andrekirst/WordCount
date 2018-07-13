using System.Collections.Generic;
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

            List<string> words = text.SplitByRegex(pattern: @"((\b[^\s\d]+\b)((?<=\.\w).)?)");
            return new TextSplitResult(words: words);
        }
    }
}
