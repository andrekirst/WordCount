using System.Globalization;
using AutoFixture.Xunit2;
using Moq;
using WordCount.Implementations.Output;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;
using Xunit;

namespace WordCount.Tests;

public class StatisticsOutputTests
{
    [Theory, AutoMoqData]
    public void StatisticsOutputTests_WriteNumberOfWords(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        StatisticsOutput sut)
    {
        languageResource
            .Setup(m => m.GetResourceStringById("NUMBER_OF_WORDS"))
            .Returns("Anzahl Wörter");

        sut.WriteNumberOfWords(2, 20);

        displayOutput.Verify(v => v.WriteLine("- Anzahl Wörter....... 2"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void StatisticsOutputTests_WriteNumberOfUniqeWords(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        StatisticsOutput sut)
    {
        languageResource
            .Setup(m => m.GetResourceStringById("UNIQUE"))
            .Returns("Eindeutig");

        sut.WriteNumberOfUniqeWords(2, 20);

        displayOutput.Verify(v => v.WriteLine("- Eindeutig........... 2"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void StatisticsOutputTests_WriteAverageWordLength(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        StatisticsOutput sut)
    {
        languageResource
            .Setup(m => m.GetResourceStringById("AVERAGE_WORD_LENGTH"))
            .Returns("Durchschnittliche Wortlänge");

        languageResource
            .Setup(m => m.GetResourceStringById("CHARACTERS"))
            .Returns("Zeichen");

        languageDecision
            .Setup(m => m.DecideLanguage())
            .Returns(new DecideLanguageResult {Culture = CultureInfo.GetCultureInfo("de-DE")});

        sut.WriteAverageWordLength(2.52, 30);

        displayOutput.Verify(v => v.WriteLine("- Durchschnittliche Wortlänge... 2,52 Zeichen"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void StatisticsOutputTests_WriteNumberOfChapters(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        StatisticsOutput sut)
    {
        languageResource
            .Setup(m => m.GetResourceStringById("CHAPTERS"))
            .Returns("Kapitel");

        sut.WriteNumberOfChapters(3, 20);

        displayOutput.Verify(v => v.WriteLine("- Kapitel............. 3"), Times.Once);
    }
}