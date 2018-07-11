using System;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public abstract class BaseParameterParser<T>
        where T : BaseParameter
    {
        private T Cache { get; set; }

        public T CachedValue(Func<T> toCachingValue)
        {
            if (Cache != null)
            {
                return Cache;
            }
            
            Cache = toCachingValue();
            return Cache;
        }
    }
}