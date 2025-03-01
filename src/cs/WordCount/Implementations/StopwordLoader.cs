using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;

namespace WordCount.Implementations;

public class StopwordLoader(
    IFileSystem fileSystem,
    IParameterParser<StopwordListParameter> stopwordListParameterParser,
    IDisplayOutput displayOutput,
    IParameterParser<LanguageParameter> languageParameterParser) : IStopwordLoader
{
    public List<string> GetStopwords()
    {
        var args = Environment.GetCommandLineArgs();
        var stopwordListParameter = stopwordListParameterParser.ParseParameter(args);
        var languageParameter = languageParameterParser.ParseParameter(args);

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
