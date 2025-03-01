using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using WordCount.Helpers;

namespace WordCount;

public interface IIndexOutput
{
    void OutputIndex(IndexOutputRequest indexOutputRequest);
}

public class IndexOutput(
    IDisplayOutput displayOutput,
    IDictionaryFileLoader dictionaryFileLoader,
    IOptions<WordCountCommand.Settings> settings) : IIndexOutput
{
    private readonly WordCountCommand.Settings _settings = settings.Value;

    public void OutputIndex(IndexOutputRequest indexOutputRequest)
    {
        if(!_settings.Index)
        {
            return;
        }
        
        var dictionaryWords = dictionaryFileLoader.ReadWords();

        var unknwonWordsCount = EnumerableHelpers.CountUnknownWords(
            indexOutputRequest.DistinctWords,
            dictionaryWords);

        if (!string.IsNullOrEmpty(_settings.Dictionary))
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
    public List<string> DistinctWords { get; set; } = [];
}
