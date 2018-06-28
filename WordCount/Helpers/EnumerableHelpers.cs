using System.Collections.Generic;
using System.Linq;

namespace WordCount.Helpers
{
    public static class EnumerableHelpers
    {
        public static int CountUnknownWords(
            IEnumerable<string> distinctWords,
            IEnumerable<string> dictionaryWords)
        {
            if (distinctWords == null)
            {
                return 0;
            }

            if (dictionaryWords == null)
            {
                return distinctWords.Count();
            }

            return distinctWords
                .Except(second: dictionaryWords)
                .Count();
        }
    }
}
