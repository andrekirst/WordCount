using System.Collections.Generic;
using System.Linq;
using WordCount.Helpers;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Models.Requests;

namespace WordCount.Implementations
{
    public class IndexOutput : IIndexOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private readonly IDictionaryFileLoader _dictionaryFileLoader;
        private readonly IIndexParameterParser _indexParameterParser;
        private readonly IDictionaryParameterParser _dictionaryParameterParser;

        public IndexOutput(
            IDisplayOutput displayOutput,
            IDictionaryFileLoader dictionaryFileLoader,
            IIndexParameterParser indexParameterParser,
            IDictionaryParameterParser dictionaryParameterParser)
        {
            _displayOutput = displayOutput;
            _dictionaryFileLoader = dictionaryFileLoader;
            _indexParameterParser = indexParameterParser;
            _dictionaryParameterParser = dictionaryParameterParser;
        }

        public void OutputIndex(IndexOutputRequest indexOutputRequest)
        {
            IndexParameter indexParameter = _indexParameterParser.ParseIndexParameter();
            DictionaryParameter dictionaryParameter = _dictionaryParameterParser.ParseDictionaryParameter();

            if (indexParameter.IsPresent)
            {
                List<string> dictionaryWords = _dictionaryFileLoader.ReadWords();

                int unknwonWordsCount = EnumerableHelpers.CountUnknownWords(
                    distinctWords: indexOutputRequest.DistinctWords,
                    dictionaryWords: dictionaryWords);

                //string indexOutputText = dictionaryParameter.IsPresent
                //    ? $"Index: (unknown: {unknwonWordsCount})"
                //    : "Index:";
                //_displayOutput.WriteLine(text: indexOutputText);
                if (dictionaryParameter.IsPresent)
                {
                    _displayOutput.WriteResourceStringWithValuesLine(
                        resourceIdent: "INDEX");
                }
                //string resourceIdentIndex = dictionaryParameter.IsPresent ? "INDEX_WITH_UNKNOWN" : "INDEX";
                

                DisplayWords(
                    distinctWords: indexOutputRequest.DistinctWords,
                    dictionaryWords: dictionaryWords);
            }
        }

        private void DisplayWords(
            List<string> distinctWords,
            List<string> dictionaryWords)
        {
            bool checkAgainstDictionary = dictionaryWords != null && dictionaryWords.Any();
            IEnumerable<string> sortedListOfDistinctWords = distinctWords.OrderBy(keySelector: s => s);

            foreach (string distinctWord in sortedListOfDistinctWords)
            {
                string word = distinctWord;
                if (checkAgainstDictionary && !dictionaryWords.Contains(item: distinctWord))
                {
                    word += "*";
                }
                _displayOutput.WriteLine(text: word);
            }
        }
    }
}
