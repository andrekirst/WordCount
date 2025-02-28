using System.Collections.Generic;
using System.IO.Abstractions;
using WordCount.Extensions;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;

namespace WordCount.Implementations;

public class DictionaryFileLoader(
    IFileSystem fileSystem,
    IDisplayOutput displayOutput,
    IDictionaryParameterParser dictionaryParameterParser) : IDictionaryFileLoader
{
    private IFileSystem FileSystem { get; } = fileSystem;
    private IDisplayOutput DisplayOutput { get; } = displayOutput;
    private IDictionaryParameterParser DictionaryParameterParser { get; } = dictionaryParameterParser;

    public List<string> ReadWords()
    {
        var dictionaryParameter = DictionaryParameterParser.ParseDictionaryParameter();

        var path = dictionaryParameter.FileName;

        switch (dictionaryParameter.IsPresent)
        {
            case true when
                !FileSystem.File.Exists(path):
                DisplayOutput.WriteErrorResourceLine(
                    "FILE_NOT_FOUND",
                    path);
                return [];
            case false:
                return [];
            default:
                return FileSystem
                    .File
                    .ReadAllLines(path)
                    .ToEmptyIfNullList();
        }
    }
}
