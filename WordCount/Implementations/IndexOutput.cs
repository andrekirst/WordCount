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
            List<string> dictionaryWords = ExtractDictionaryWords(indexOutputRequest: indexOutputRequest);
            int unknwonWordsCount = CountUnknownWords(
                distinctWords: indexOutputRequest.DistinctWords,
                dictionaryWords: dictionaryWords);

            string indexOutputText = indexOutputRequest.IsDictionaryParameterPresent
                ? $"Index: (unknown: {unknwonWordsCount})"
                : "Index:";

            _displayOutput.WriteLine(text: indexOutputText);
            DisplayWords(
                distinctWords: indexOutputRequest.DistinctWords,
                dictionaryWords: dictionaryWords);
        }

        private void DisplayWords(
            IEnumerable<string> distinctWords,
            List<string> dictionaryWords)
        {
            bool checkAgainstDictionary = dictionaryWords.Any();

            foreach (string distinctWord in distinctWords
                            .OrderBy(keySelector: s => s))
            {
                string word = distinctWord;
                if (checkAgainstDictionary &&
                    !dictionaryWords.Contains(item: distinctWord))
                {
                    word += "*";
                }
                _displayOutput.WriteLine(text: word);
            }
        }

        private static int CountUnknownWords(
            IEnumerable<string> distinctWords,
            IEnumerable<string> dictionaryWords)
        {
            return distinctWords
                .Except(second: dictionaryWords)
                .Count();
        }

        private List<string> ExtractDictionaryWords(IndexOutputRequest indexOutputRequest)
        {
            List<string> dictionaryWords = new List<string>();

            if (indexOutputRequest.IsDictionaryParameterPresent)
            {
                dictionaryWords.AddRange(collection: _dictionaryFileLoader.ReadWords(path: indexOutputRequest.DictionaryTextFile));
            }

            return dictionaryWords;
        }
    }
}
