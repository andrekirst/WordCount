using System;
using System.IO.Abstractions;
using Microsoft.Extensions.Options;

namespace WordCount;

public interface ITextFileLoader
{
    string ReadTextFile();
}

public class TextFileLoader(
    IFileSystem fileSystem,
    IDisplayOutput displayOutput,
    IOptions<WordCountCommand.Settings> settings) : ITextFileLoader
{
    private readonly WordCountCommand.Settings _settings = settings.Value;

    public string ReadTextFile()
    {
        var fileName = _settings.SourceFile;

        if (fileName is null)
        {
            return string.Empty;
        }

        if (fileSystem.File.Exists(fileName))
        {
            return fileSystem.File.ReadAllText(fileName)
                .Replace(
                $"-{Environment.NewLine}",
                string.Empty);
        }

        displayOutput.WriteErrorResourceLine("FILE_NOT_FOUND", fileName);
        
        return string.Empty;
    }
}
