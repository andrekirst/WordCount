using System;
using System.Collections.Generic;
using WordCount.Implementations;
using WordCount.Models.Results;
using Xunit;

namespace WordCount.Tests;

public class TextSplitTests
{
    private readonly TextSplit _systemUnderTest;

    public TextSplitTests()
    {
        _systemUnderTest = new TextSplit();
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Analyze_Text_Bla_bla_Expect_2_Words()
    {
        TextSplitResult actual = _systemUnderTest.Split("Bla bla");

        Assert.Equal(2, actual.Words.Count);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Analyze_Text_a_with_dot_Expect_1_Word()
    {
        TextSplitResult actual = _systemUnderTest.Split("a.");

        Assert.Single(actual.Words);
        Assert.Equal("a", actual.Words[0]);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Analyze_Stopword_a_This_is_a_Text_Expect_4_Words()
    {
        TextSplitResult actual = _systemUnderTest.Split(
            "This is a Text");

        Assert.Equal(4, actual.Words.Count);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Analyze_Stopword_a_Mary_has_a_little_lamb_with_Newlines_Expect_5_Words()
    {
        TextSplitResult actual = _systemUnderTest.Split(
            $"Mary had{Environment.NewLine}a little{Environment.NewLine}lamb");

        Assert.Equal(5, actual.Words.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void TextSplitTests_Analyze_empty_strings_Expect_0_Words(
        string inputtext)
    {
        TextSplitResult actual = _systemUnderTest.Split(inputtext);

        Assert.NotNull(actual);
        Assert.NotNull(actual.Words);
        Assert.Empty(actual.Words);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void TextSplitTests_empty_strings_Expect_ValuesAvailable_false(string inputtext)
    {
        TextSplitResult actual = _systemUnderTest.Split(inputtext);

        Assert.False(actual.WordsAvailable);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Humpty_Dumpty_Text_Expect_10_Words()
    {
        const string text = "Humpty-Dumpty sat on a wall. Humpty-Dumpty had a great fall.";
        TextSplitResult actual = _systemUnderTest.Split(text);

        Assert.Equal(
            10,
            actual.Words.Count);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Text_with_Unicode_Expect_2_Words()
    {
        const string text = "Hello André";
        TextSplitResult actual = _systemUnderTest.Split(text);

        List<string> expected = new List<string> { "Hello", "André" };

        Assert.Equal(
            expected,
            actual.Words);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Text_with_MinusAtEnd_Expect_word_Without_Minus()
    {
        const string text = "pre- pre-condition";
        TextSplitResult actual = _systemUnderTest.Split(text);

        List<string> expected = new List<string> { "pre", "pre-condition" };

        Assert.Equal(
            expected,
            actual.Words);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Text_with_Umlaute_Expect_Text_With_Umlaute()
    {
        const string text = "Draußen ist es schön";
        TextSplitResult actual = _systemUnderTest.Split(text);

        List<string> expected = new List<string> { "Draußen", "ist", "es", "schön" };

        Assert.Equal(
            expected,
            actual.Words);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Text_with_Digit_Expect_without_Digit()
    {
        const string text = "Draußen ist es schön 1";
        TextSplitResult actual = _systemUnderTest.Split(text);

        List<string> expected = new List<string> { "Draußen", "ist", "es", "schön" };

        Assert.Equal(
            expected,
            actual.Words);
    }

    [Theory, AutoMoqData]
    public void TextSplitTests_Words_with_Comma_expect_no_Comma()
    {
        const string text = "Longworth, however, in desperate need of money, killed the preacher with Hamlet's sword and  stole the manuscript";
        TextSplitResult actual = _systemUnderTest.Split(text);

        List<string> expected = new List<string>() { "Longworth", "however", "in", "desperate", "need", "of", "money", "killed", "the", "preacher", "with", "Hamlet's", "sword", "and", "stole", "the", "manuscript" };
        Assert.Equal(expected,
            actual.Words);
    }
}