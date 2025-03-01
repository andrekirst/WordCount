﻿using System;
using System.IO.Abstractions;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;

namespace WordCount.Implementations;

public class TextFileLoader(
    IFileSystem fileSystem,
    IDisplayOutput displayOutput,
    IParameterParser<SourceFileParameter> sourceFileParameterParser) : ITextFileLoader
{
    public string ReadTextFile()
    {
        var args = Environment.GetCommandLineArgs();
        var sourceFileParameter = sourceFileParameterParser.ParseParameter(args);

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
