using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;

namespace WordCount.Implementations;

public class StopwordLoader(
    IFileSystem fileSystem,
    IStopwordListParameterParser stopwordListParameterParser,
    IDisplayOutput displayOutput,
    ILanguageParameterParser languageParameterParser) : IStopwordLoader
{
    public List<string> GetStopwords()
    {
        var stopwordListParameter = stopwordListParameterParser.ParseStopwordListParameter();
        var languageParameter = languageParameterParser.ParseLanguageParameter();

        var isStopwordListParameterPresent = stopwordListParameter.IsPresent;

        var fileName = isStopwordListParameterPresent ?
            stopwordListParameter.FileName :
            $"stopwords.{languageParameter.Language}.txt";

        if (!fileSystem.File.Exists(fileName))
        {
            return [];
        }

        if (isStopwordListParameterPresent)
        {
            displayOutput.WriteResourceLine("USED_STOPWORDLIST", fileName);
        }

        return fileSystem
            .File
            .ReadAllLines(fileName)
            .ToList();
    }
}
