﻿using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IStopwordListParameterParser
    {
        StopwordListParameter ParseStopwordListParameter();
    }
}
