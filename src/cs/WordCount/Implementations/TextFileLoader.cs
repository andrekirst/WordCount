using System;
using System.IO.Abstractions;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;

namespace WordCount.Implementations;

public class TextFileLoader(
    IFileSystem fileSystem,
    IDisplayOutput displayOutput,
    ISourceFileParameterParser sourceFileParameterParser) : ITextFileLoader
{
    public string ReadTextFile()
    {
        var sourceFileParameter = sourceFileParameterParser.ParseSourceFileParameter();

        if (!sourceFileParameter.IsPresent)
        {
            return string.Empty;
        }

        var fileName = sourceFileParameter.FileName;

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
