using System;
using System.Linq;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class IndexParameterParser : BaseParameterParser<IndexParameter>, IParameterParser<IndexParameter>
{
    public IndexParameter ParseParameter(string[] args)
    {
        return CachedValue(() =>
        {            
            return new IndexParameter
            {
                IsPresent = args.Contains("-index")
            };
        });
    }
}
