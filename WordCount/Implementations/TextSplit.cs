using System;
using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class TextSplit : ITextSplit
    {
        public TextSplitResult Split(string text)
        {
            if (string.IsNullOrWhiteSpace(value: text))
            {
                return new TextSplitResult(new List<string>());
            }

            List<string> splitByWhitespace = text.Split(
                separator: new[] { " ", Environment.NewLine },
                options: StringSplitOptions.None).ToList();

            return new TextSplitResult(values: splitByWhitespace);
        }
    }
}
