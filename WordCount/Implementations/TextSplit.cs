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

            List<string> words = text.SplitByRegex(pattern: @"[a-zA-Z'\-\u00D8-\u00F6]{0,}[^"":<> ,-\.0-9(\r|\n)]");

            return new TextSplitResult(words: words);
        }
    }
}
