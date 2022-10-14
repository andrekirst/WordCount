using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Implementations;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;
using Xunit;

namespace WordCount.Tests;

public class StopwordLoaderTests
{
    [Theory, AutoMoqData]
    public void StopwordLoaderTests_GetStopwords_FileNotExist_Return_EmptyList(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IStopwordListParameterParser> stopwordListParameterParser,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<IFileSystem> fileSystem,
        StopwordLoader sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { Language = "en" });

        stopwordListParameterParser
            .Setup(m => m.ParseStopwordListParameter())
            .Returns(new StopwordListParameter { IsPresent = false });

        fileSystem
            .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
            .Throws(new FileNotFoundException());

        var actual = sut.GetStopwords();

        actual.Should().NotBeNull();

        displayOutput.Verify(v => v.WriteLine(It.IsAny<string>()), Times.Never);
    }

    [Theory, AutoMoqData]
    public void StopwordLoaderTests_GetStopwords_FileEmpty_Return_EmptyList(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IStopwordListParameterParser> stopwordListParameterParser,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<IFileSystem> fileSystem,
        StopwordLoader sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { Language = "en" });

        stopwordListParameterParser
            .Setup(m => m.ParseStopwordListParameter())
            .Returns(new StopwordListParameter { IsPresent = false });

        fileSystem
            .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
            .Returns(Array.Empty<string>());

        var actual = sut.GetStopwords();

        actual.Should().NotBeNull();

        displayOutput.Verify(v => v.WriteLine(It.IsAny<string>()), Times.Never);
    }

    [Theory, AutoMoqData]
    public void StopwordLoaderTests_GetStopwords_Contains_1_Row_With_word_a_Return_List_with_one_Entry_a(
        List<string> lines,
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IStopwordListParameterParser> stopwordListParameterParser,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<IFileSystem> fileSystem,
        StopwordLoader sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { Language = "en" });

        stopwordListParameterParser
            .Setup(m => m.ParseStopwordListParameter())
            .Returns(new StopwordListParameter { IsPresent = false });

        fileSystem
            .Setup(m => m.File.Exists(It.IsAny<string>()))
            .Returns(true);

        fileSystem
            .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
            .Returns(lines.ToArray);

        var actual = sut.GetStopwords();

        actual.Should().BeEquivalentTo(lines);

        displayOutput.Verify(v => v.WriteLine(It.IsAny<string>()), Times.Never);
    }

    [Theory, AutoMoqData]
    public void StopwordLoaderTests_StopwordParameter_is_present_take_StopwordParameter_FileName(
        string filename,
        [Frozen] Mock<IStopwordListParameterParser> stopwordListParameterParser,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<IFileSystem> fileSystem,
        StopwordLoader sut)
    {
        stopwordListParameterParser
            .Setup(m => m.ParseStopwordListParameter())
            .Returns(new StopwordListParameter { IsPresent = true, FileName = filename });

        fileSystem
            .Setup(m => m.File.Exists(filename))
            .Returns(true);

        sut.GetStopwords();

        fileSystem.Verify(v => v.File.Exists(filename), Times.Once);
        fileSystem.Verify(v => v.File.ReadAllLines(filename), Times.Once);
        displayOutput.Verify(v => v.WriteResourceLine("USED_STOPWORDLIST", filename), Times.Once);
    }

    [Theory, AutoMoqData]
    public void StopwordLoaderTests_StopwordParameter_is_not_present_language_is_de_expect_loading_de_file(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IStopwordListParameterParser> stopwordListParameterParser,
        [Frozen] Mock<IFileSystem> fileSystem,
        StopwordLoader sut)
    {
        stopwordListParameterParser
            .Setup(m => m.ParseStopwordListParameter())
            .Returns(new StopwordListParameter { IsPresent = false });

        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { Language = "de" });

        fileSystem
            .Setup(m => m.File.Exists(It.IsAny<string>()))
            .Returns(true);

        sut.GetStopwords();

        fileSystem.Verify(v => v.File.Exists("stopwords.de.txt"), Times.Once);
        fileSystem.Verify(v => v.File.ReadAllLines("stopwords.de.txt"), Times.Once);
    }
}