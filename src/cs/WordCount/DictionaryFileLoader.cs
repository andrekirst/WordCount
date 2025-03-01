using System.Collections.Generic;
using System.IO.Abstractions;
using Microsoft.Extensions.Options;
using WordCount.Extensions;

namespace WordCount;

public interface IDictionaryFileLoader
{
    List<string> ReadWords();
}

public class DictionaryFileLoader(
    IFileSystem fileSystem,
    IDisplayOutput displayOutput,
    IOptions<WordCountCommand.Settings> settings) : IDictionaryFileLoader
{
    private readonly WordCountCommand.Settings _settings = settings.Value;

    public List<string> ReadWords()
    {
        var path = _settings.Dictionary;

        switch (!string.IsNullOrEmpty(path))
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
