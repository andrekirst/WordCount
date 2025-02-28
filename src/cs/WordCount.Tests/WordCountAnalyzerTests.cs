using System;
using Moq;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models.Results;
using Xunit;

namespace WordCount.Tests;

public class WordCountAnalyzerTests
{
    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Analyze_Text_Bla_bla_Expect_2_Words(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        const string text = "Bla bla";
        var mockTextSplitResulValues = new List<string> { "Bla", "bla" };

        stopwordRemover
            .Setup(m => m.RemoveStopwords(It.IsAny<List<string>>()))
            .Returns(new StopwordRemoverResult { Words = mockTextSplitResulValues });

        textSplit
            .Setup(m => m.Split(text))
            .Returns(new TextSplitResult(mockTextSplitResulValues));

        var actual = sut.Analyze(text);

        actual.NumberOfWords.Should().Be(2);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Analyze_Stopword_a_This_is_a_Text_Expect_3_Words(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        const string text = "This is a Text";

        var values = new List<string> { "This", "is", "a", "Text" };

        textSplit
            .Setup(m => m.Split(text))
            .Returns(new TextSplitResult(values));

        stopwordRemover
            .Setup(m => m.RemoveStopwords(values))
            .Returns(new StopwordRemoverResult
            {
                Words = new List<string> { "This", "is", "Text" }
            });

        var actual = sut.Analyze(text);

        actual.NumberOfWords.Should().Be(3);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Analyze_empty_strings_Expect_0_Words(
        Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        const string inputtext = "";
        textSplit
            .Setup(m => m.Split(inputtext))
            .Returns(new TextSplitResult(new List<string>()));

        var actual = sut.Analyze(inputtext);

        actual.NumberOfWords.Should().Be(0);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Long_text_Bla_bla_bla_Expect_Number_of_Words_3(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        const string text = "Bla bla bla";
        var mockValues = new List<string> { "Bla", "bla", "bla" };
        var mockValuesStopwordsRemoved = new List<string> { "Bla", "bla", "bla" };

        textSplit
            .Setup(m => m.Split(text))
            .Returns(new TextSplitResult(mockValues));

        stopwordRemover
            .Setup(m => m.RemoveStopwords(mockValues))
            .Returns(new StopwordRemoverResult { Words = mockValuesStopwordsRemoved });

        var actual = sut.Analyze(text);

        actual.NumberOfWords.Should().Be(3);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Long_text_Bla_bla_bla_Expect_Number_of_unique_Words_2(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        const string text = "Bla bla bla";

        var mockValues = new List<string> { "Bla", "bla", "bla" };

        textSplit
            .Setup(m => m.Split(text))
            .Returns(new TextSplitResult(mockValues));

        stopwordRemover
            .Setup(m => m.RemoveStopwords(mockValues))
            .Returns(new StopwordRemoverResult { Words = mockValues });

        var actual = sut.Analyze(text);

        actual.NumberOfUniqueWords.Should().Be(2);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Text_Bla_bla_bla_Expect_Average_word_Length_3(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        const string text = "Bla bla bla";

        var mockValues = new List<string> { "Bla", "bla", "bla" };

        textSplit
            .Setup(m => m.Split(text))
            .Returns(new TextSplitResult(mockValues));

        stopwordRemover
            .Setup(m => m.RemoveStopwords(mockValues))
            .Returns(new StopwordRemoverResult { Words = mockValues });

        var actual = sut.Analyze(text);

        actual.AverageWordLength.Should().Be(3.0);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Text_Bla_bla_bla_Expect_distinct_Words_Bla_bla(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        const string text = "Bla bla bla";

        var mockValues = new List<string> { "Bla", "bla", "bla" };

        textSplit
            .Setup(m => m.Split(text))
            .Returns(new TextSplitResult(mockValues));

        stopwordRemover
            .Setup(m => m.RemoveStopwords(mockValues))
            .Returns(new StopwordRemoverResult { Words = mockValues });

        var expected = new List<string> { "Bla", "bla" };

        var actual = sut.Analyze(text);

        actual.DistinctWords.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Text_a_Stopword_a_Expect_All_Values_in_Result_0(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        stopwordRemover
            .Setup(m => m.RemoveStopwords(It.IsAny<List<string>>()))
            .Returns(new StopwordRemoverResult());

        textSplit
            .Setup(m => m.Split("a."))
            .Returns(new TextSplitResult(new List<string> { "a" }));

        var actual = sut.Analyze("a.");

        actual.Should().NotBeNull();
        actual.AverageWordLength.Should().Be(0.0);
        actual.NumberOfUniqueWords.Should().Be(0);
        actual.NumberOfWords.Should().Be(0);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Text_with_2_Linebreaks_Expect_2_chapters(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        textSplit
            .Setup(m => m.Split(It.IsAny<string>()))
            .Returns(new TextSplitResult(new List<string> { "asd" }));

        stopwordRemover
            .Setup(m => m.RemoveStopwords(It.IsAny<List<string>>()))
            .Returns(new StopwordRemoverResult { Words = new List<string> { "asd" } });

        var inputText =
            $"Das ist das erste Kapitel.{Environment.NewLine}{Environment.NewLine}Das ist das zweite Kapitel.";

        var actual = sut.Analyze(inputText);

        actual.NumberOfChapters.Should().Be(2);
    }

    [Theory, AutoMoqData]
    public void WordCountAnalyzerTests_Text_with_1_Linebreak_Expect_1_chapters(
        [Frozen] Mock<IStopwordRemover> stopwordRemover,
        [Frozen] Mock<ITextSplit> textSplit,
        WordCountAnalyzer sut)
    {
        textSplit
            .Setup(m => m.Split(It.IsAny<string>()))
            .Returns(new TextSplitResult(new List<string> { "asd" }));

        stopwordRemover
            .Setup(m => m.RemoveStopwords(It.IsAny<List<string>>()))
            .Returns(new StopwordRemoverResult { Words = new List<string> { "asd" } });

        var inputText = $"Das ist das erste Kapitel.{Environment.NewLine}Das ist das zweite Kapitel.";

        var actual = sut.Analyze(inputText);

        actual.NumberOfChapters.Should().Be(1);
    }
}