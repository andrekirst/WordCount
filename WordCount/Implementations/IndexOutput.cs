using System.Collections.Generic;
using System.Linq;
using WordCount.Helpers;
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
            int unknwonWordsCount = EnumerableHelpers.CountUnknownWords(
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

        // TODO Eventuell auch nochmal als Schnittstelle
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

        // TODO Eventuell auch nochmal als Schnittstelle
        private List<string> ExtractDictionaryWords(IndexOutputRequest indexOutputRequest)
        {
            List<string> dictionaryWords = new List<string>();

            if (indexOutputRequest.IsDictionaryParameterPresent)
            {
                dictionaryWords.AddRange(collection: _dictionaryFileLoader.ReadWords());
            }

            return dictionaryWords;
        }
    }
}
