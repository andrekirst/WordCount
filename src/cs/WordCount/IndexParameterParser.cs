using System;
using System.Linq;
using WordCount.Models.Parameters;

namespace WordCount;

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
