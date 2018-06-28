using System.Collections.Generic;
using WordCount.Interfaces;
using WordCount.Models;
using WordCount.Extensions;

namespace WordCount.Implementations
{
    public class TextSplit : ITextSplit
    {
        public TextSplitResult Split(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return new TextSplitResult(words: new List<string>());
            }

            List<string> words = text.SplitByRegex(pattern: @"[a-zA-Z\-\u00D8-\u00F6]{0,}[^-\s\d]");

            return new TextSplitResult(words: words);
        }
    }
}
