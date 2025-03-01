using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Microsoft.Extensions.Options;

namespace WordCount;

public interface IStopwordLoader
{
    List<string> GetStopwords();
}

public class StopwordLoader(
    IFileSystem fileSystem,
    IDisplayOutput displayOutput,
    IOptions<WordCountCommand.Settings> settings) : IStopwordLoader
{
    private readonly WordCountCommand.Settings _settings = settings.Value;

    public List<string> GetStopwords()
    {
        var stopwordList = _settings.StopwordList;
        var isStopwordListParameterPresent = !string.IsNullOrEmpty(stopwordList);

        var fileName = !string.IsNullOrEmpty(stopwordList) ?
            stopwordList :
            $"stopwords.{_settings.Language}.txt";

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
