using System.Collections.Generic;
using System.Linq;
using WordCount.Helpers;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Requests;

namespace WordCount.Implementations.Output;

public class IndexOutput(
    IDisplayOutput displayOutput,
    IDictionaryFileLoader dictionaryFileLoader,
    IIndexParameterParser indexParameterParser,
    IDictionaryParameterParser dictionaryParameterParser) : IIndexOutput
{
    public void OutputIndex(IndexOutputRequest indexOutputRequest)
    {
        var indexParameter = indexParameterParser.ParseIndexParameter();
        var dictionaryParameter = dictionaryParameterParser.ParseDictionaryParameter();

        if (!indexParameter.IsPresent) return;
        
        var dictionaryWords = dictionaryFileLoader.ReadWords();

        var unknwonWordsCount = EnumerableHelpers.CountUnknownWords(
            indexOutputRequest.DistinctWords,
            dictionaryWords);

        if (dictionaryParameter.IsPresent)
        {
            displayOutput.WriteResourceLine("INDEX_WITH_UNKNOWN", unknwonWordsCount);
        }
        else
        {
            displayOutput.WriteResourceLine("INDEX");
        }

        DisplayWords(indexOutputRequest.DistinctWords, dictionaryWords);
    }

    private void DisplayWords(
        IEnumerable<string> distinctWords,
        ICollection<string> dictionaryWords)
    {
        var checkAgainstDictionary = dictionaryWords != null && dictionaryWords.Count != 0;
        IEnumerable<string> sortedListOfDistinctWords = distinctWords.OrderBy(s => s);

        foreach (var distinctWord in sortedListOfDistinctWords)
        {
            var word = distinctWord;
            if (checkAgainstDictionary && !dictionaryWords.Contains(distinctWord))
            {
                word += "*";
            }
            displayOutput.WriteLine(word);
        }
    }
}
