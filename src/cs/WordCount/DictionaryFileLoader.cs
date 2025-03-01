using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using WordCount.Extensions;
using WordCount.Models.Parameters;

namespace WordCount;

public interface IDictionaryFileLoader
{
    List<string> ReadWords();
}

public class DictionaryFileLoader(
    IFileSystem fileSystem,
    IDisplayOutput displayOutput,
    IParameterParser<DictionaryParameter> dictionaryParameterParser) : IDictionaryFileLoader
{
    public List<string> ReadWords()
    {
        var args = Environment.GetCommandLineArgs();
        var dictionaryParameter = dictionaryParameterParser.ParseParameter(args);

        var path = dictionaryParameter.FileName;

        switch (dictionaryParameter.IsPresent)
        {
            case true when
                !fileSystem.File.Exists(path):
                displayOutput.WriteErrorResourceLine(
                    "FILE_NOT_FOUND",
                    path);
                return [];
            case false:
                return [];
            default:
                return fileSystem
                    .File
                    .ReadAllLines(path)
                    .ToEmptyIfNullList();
        }
    }
}
