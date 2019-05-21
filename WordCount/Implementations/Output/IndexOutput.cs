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
            IndexParameter indexParameter = IndexParameterParser.ParseIndexParameter();
            DictionaryParameter dictionaryParameter = DictionaryParameterParser.ParseDictionaryParameter();

            if (indexParameter.IsPresent)
            {
                List<string> dictionaryWords = DictionaryFileLoader.ReadWords();

                int unknwonWordsCount = EnumerableHelpers.CountUnknownWords(
                    distinctWords: indexOutputRequest.DistinctWords,
                    dictionaryWords: dictionaryWords);

                if (dictionaryParameter.IsPresent)
                {
                    DisplayOutput.WriteResourceLine(
                        resourceIdent: "INDEX_WITH_UNKNOWN",
                        placeholderValues: unknwonWordsCount);
                }
                else
                {
                    DisplayOutput.WriteResourceLine(
                        resourceIdent: "INDEX");
                }

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
                DisplayOutput.WriteLine(text: word);
            }
        }
    }
}
