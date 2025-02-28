using System;
using Moq;
using System.IO.Abstractions;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Implementations;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;
using Xunit;

namespace WordCount.Tests;

public class TextFileLoaderTests
{
    [Theory, AutoMoqData]
    public void TextFileLoaderTests_FileNotExist_True_Expect_Output_and_return_empty_string(
        [Frozen] Mock<ISourceFileParameterParser> sourceFileParameterParser,
        [Frozen] Mock<IFileSystem> fileSystem,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        TextFileLoader sut)
    {
        sourceFileParameterParser
            .Setup(m => m.ParseSourceFileParameter())
            .Returns(new SourceFileParameter
            {
                IsPresent = true,
                FileName = "datei1.txt"
            });

        fileSystem
            .Setup(m => m.File.Exists(It.IsAny<string>()))
            .Returns(false);

        var actual = sut.ReadTextFile();

        actual.Should().BeEmpty();

        displayOutput.Verify(v => v.WriteErrorResourceLine("FILE_NOT_FOUND", "datei1.txt"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void TextFileLoaderTests_ReadAllText_Bla_Expect_Bla(
        [Frozen] Mock<ISourceFileParameterParser> sourceFileParameterParser,
        [Frozen] Mock<IFileSystem> fileSystem,
        TextFileLoader sut)
    {
        sourceFileParameterParser
            .Setup(m => m.ParseSourceFileParameter())
            .Returns(new SourceFileParameter() { IsPresent = true, FileName = It.IsAny<string>() });

        fileSystem
            .Setup(m => m.File.ReadAllText(It.IsAny<string>()))
            .Returns("Bla");

        fileSystem
            .Setup(m => m.File.Exists(It.IsAny<string>()))
            .Returns(true);

        var actual = sut.ReadTextFile();

        actual.Should().Be("Bla");
    }

    [Theory, AutoMoqData]
    public void TextFileLoaderTests_Text_mit_neuer_Zeile_und_Bindestrich_zu_Text_ohne_Bindestrich_und_vollem_Wort(
        [Frozen] Mock<ISourceFileParameterParser> sourceFileParameterParser,
        [Frozen] Mock<IFileSystem> fileSystem,
        TextFileLoader sut)
    {
        var inputText = $"Das ist ein lan-{Environment.NewLine}ger Text";

        sourceFileParameterParser
            .Setup(m => m.ParseSourceFileParameter())
            .Returns(new SourceFileParameter() {IsPresent = true, FileName = It.IsAny<string>()});

        fileSystem
            .Setup(m => m.File.Exists(It.IsAny<string>()))
            .Returns(true);

        fileSystem
            .Setup(m => m.File.ReadAllText(It.IsAny<string>()))
            .Returns(inputText);

        var actual = sut.ReadTextFile();

        const string expected = "Das ist ein langer Text";

        actual.Should().Be(expected);
    }

    [Theory, AutoMoqData]
    public void TextFileLoaderTests_Parameter_is_not_present_do_not_call_file_exist(
        [Frozen] Mock<ISourceFileParameterParser> sourceFileParameterParser,
        [Frozen] Mock<IFileSystem> fileSystem,
        TextFileLoader sut)
    {
        sourceFileParameterParser
            .Setup(m => m.ParseSourceFileParameter())
            .Returns(new SourceFileParameter() {IsPresent = false});

        var actual = sut.ReadTextFile();

        actual.Should().BeEmpty();

        fileSystem.Verify(v => v.File.Exists(It.IsAny<string>()), Times.Never);
    }
}