using System.Collections.Generic;
using System.Linq;
using WordCount.Helpers;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;
using WordCount.Models.Requests;

namespace WordCount.Implementations.Output
{
    public class IndexOutput : IIndexOutput
    {
        private IDisplayOutput DisplayOutput { get; }
        private IDictionaryFileLoader DictionaryFileLoader { get; }
        private IIndexParameterParser IndexParameterParser { get; }
        private IDictionaryParameterParser DictionaryParameterParser { get; }

        public IndexOutput(
            IDisplayOutput displayOutput,
            IDictionaryFileLoader dictionaryFileLoader,
            IIndexParameterParser indexParameterParser,
            IDictionaryParameterParser dictionaryParameterParser)
        {
            DisplayOutput = displayOutput;
            DictionaryFileLoader = dictionaryFileLoader;
            IndexParameterParser = indexParameterParser;
            DictionaryParameterParser = dictionaryParameterParser;
        }

        public void OutputIndex(IndexOutputRequest indexOutputRequest)
        {
            var indexParameter = IndexParameterParser.ParseIndexParameter();
            var dictionaryParameter = DictionaryParameterParser.ParseDictionaryParameter();

            if (!indexParameter.IsPresent) return;
            
            var dictionaryWords = DictionaryFileLoader.ReadWords();

            var unknwonWordsCount = EnumerableHelpers.CountUnknownWords(
                indexOutputRequest.DistinctWords,
                dictionaryWords);

            if (dictionaryParameter.IsPresent)
            {
                DisplayOutput.WriteResourceLine(
                    "INDEX_WITH_UNKNOWN",
                    unknwonWordsCount);
            }
            else
            {
                DisplayOutput.WriteResourceLine(
                    "INDEX");
            }

            DisplayWords(
                indexOutputRequest.DistinctWords,
                dictionaryWords);
        }

        private void DisplayWords(
            IEnumerable<string> distinctWords,
            ICollection<string> dictionaryWords)
        {
            var checkAgainstDictionary = dictionaryWords != null && dictionaryWords.Any();
            IEnumerable<string> sortedListOfDistinctWords = distinctWords.OrderBy(s => s);

            foreach (var distinctWord in sortedListOfDistinctWords)
            {
                var word = distinctWord;
                if (checkAgainstDictionary && !dictionaryWords.Contains(distinctWord))
                {
                    word += "*";
                }
                DisplayOutput.WriteLine(word);
            }
        }
    }
}
