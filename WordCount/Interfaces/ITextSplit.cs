﻿using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface ITextSplit
    {
        TextSplitResult Split(string text);
    }
}
