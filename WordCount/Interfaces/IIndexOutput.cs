﻿using WordCount.Models.Requests;

namespace WordCount.Interfaces
{
    public interface IIndexOutput
    {
        void OutputIndex(IndexOutputRequest indexOutputRequest);
    }
}
