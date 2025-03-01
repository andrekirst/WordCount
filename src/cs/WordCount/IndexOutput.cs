using System;
using System.Collections.Generic;
using System.Linq;
using WordCount.Helpers;
using WordCount.Models.Parameters;

namespace WordCount;

public interface IIndexOutput
{
    void OutputIndex(IndexOutputRequest indexOutputRequest);
}

public class IndexOutput(
    IDisplayOutput displayOutput,
    IDictionaryFileLoader dictionaryFileLoader,
    IParameterParser<IndexParameter> indexParameterParser,
    IParameterParser<DictionaryParameter> dictionaryParameterParser) : IIndexOutput
{
    public void OutputIndex(IndexOutputRequest indexOutputRequest)
    {
        var args = Environment.GetCommandLineArgs();
        var indexParameter = indexParameterParser.ParseParameter(args);
        var dictionaryParameter = dictionaryParameterParser.ParseParameter(args);

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

public class IndexOutputRequest
{
    public List<string> DistinctWords { get; set; }
}
