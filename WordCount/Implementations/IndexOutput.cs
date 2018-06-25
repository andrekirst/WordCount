using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class IndexOutput : IIndexOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private readonly IDictionaryFileLoader _dictionaryFileLoader;

        public IndexOutput(
            IDisplayOutput displayOutput,
            IDictionaryFileLoader dictionaryFileLoader)
        {
            _displayOutput = displayOutput;
            _dictionaryFileLoader = dictionaryFileLoader;
        }

        public void OutputIndex(IndexOutputRequest indexOutputRequest)
        {
            List<string> dictionaryWords = new List<string>();

            if (indexOutputRequest.IsDictionaryParameterPresent)
            {
                dictionaryWords.AddRange(collection: _dictionaryFileLoader.ReadWords(path: indexOutputRequest.DictionaryTextFile));
            }

            var unknwonWordsCount = indexOutputRequest
                .DistinctWords
                .Except(second: dictionaryWords)
                .Count();

            string indexOutputText = indexOutputRequest.IsDictionaryParameterPresent
                ? $"Index: (unknown: {unknwonWordsCount})"
                : "Index:";

            _displayOutput.WriteLine(text: indexOutputText);

            foreach (string distinctWord in indexOutputRequest
                .DistinctWords
                .OrderBy(keySelector: s => s))
            {
                string word = distinctWord;
                if (dictionaryWords.Contains(item: distinctWord))
                {
                    word += "*";
                }
                _displayOutput.WriteLine(text: word);
            }
        }
    }
}
