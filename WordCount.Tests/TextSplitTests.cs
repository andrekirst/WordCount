using System;
using System.Collections.Generic;
using WordCount.Implementations;
using WordCount.Models;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class TextSplitTests
    {
        private readonly TextSplit _systemUnderTest;

        public TextSplitTests()
        {
            _systemUnderTest = new TextSplit();
        }

        [NamedFact]
        public void TextSplitTests_Analyze_Text_Bla_bla_Expect_2_Words()
        {
            TextSplitResult actual = _systemUnderTest.Split(text: "Bla bla");

            Assert.Equal(expected: 2, actual: actual.Words.Count);
        }

        [NamedFact]
        public void TextSplitTests_Analyze_Text_a_with_dot_Expect_1_Word()
        {
            TextSplitResult actual = _systemUnderTest.Split(text: "a.");

            Assert.Single(collection: actual.Words);
            Assert.Equal(expected: "a", actual: actual.Words[0]);
        }

        [NamedFact]
        public void TextSplitTests_Analyze_Stopword_a_This_is_a_Text_Expect_4_Words()
        {
            TextSplitResult actual = _systemUnderTest.Split(
                text: "This is a Text");

            Assert.Equal(expected: 4, actual: actual.Words.Count);
        }

        [NamedFact]
        public void TextSplitTests_Analyze_Stopword_a_Mary_has_a_little_lamb_with_Newlines_Expect_5_Words()
        {
            TextSplitResult actual = _systemUnderTest.Split(
                text: $"Mary had{Environment.NewLine}a little{Environment.NewLine}lamb");

            Assert.Equal(expected: 5, actual: actual.Words.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TextSplitTests_Analyze_empty_strings_Expect_0_Words(
            string inputtext)
        {
            TextSplitResult actual = _systemUnderTest.Split(text: inputtext);

            Assert.NotNull(@object: actual);
            Assert.NotNull(@object: actual.Words);
            Assert.Empty(collection: actual.Words);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TextSplitTests_empty_strings_Expect_ValuesAvailable_false(string inputtext)
        {
            TextSplitResult actual = _systemUnderTest.Split(text: inputtext);

            Assert.False(condition: actual.WordsAvailable);
        }

        [NamedFact]
        public void TextSplitTests_Humpty_Dumpty_Text_Expect_10_Words()
        {
            const string text = "Humpty-Dumpty sat on a wall. Humpty-Dumpty had a great fall.";
            TextSplitResult actual = _systemUnderTest.Split(text: text);

            Assert.Equal(
                expected: 10,
                actual: actual.Words.Count);
        }

        [NamedFact]
        public void TextSplitTests_Text_with_Unicode_Expect_2_Words()
        {
            const string text = "Hello André";
            TextSplitResult actual = _systemUnderTest.Split(text: text);

            List<string> expected = new List<string> { "Hello", "André" };

            Assert.Equal(
                expected: expected,
                actual: actual.Words);
        }

        [NamedFact]
        public void TextSplitTests_Text_with_MinusAtEnd_Expect_word_Without_Minus()
        {
            const string text = "pre- pre-condition";
            TextSplitResult actual = _systemUnderTest.Split(text: text);

            List<string> expected = new List<string> { "pre", "pre-condition" };

            Assert.Equal(
                expected: expected,
                actual: actual.Words);
        }

        [NamedFact]
        public void TextSplitTests_Text_with_Umlaute_Expect_Text_With_Umlaute()
        {
            const string text = "Draußen ist es schön";
            TextSplitResult actual = _systemUnderTest.Split(text: text);

            List<string> expected = new List<string> { "Draußen", "ist", "es", "schön" };

            Assert.Equal(
                expected: expected,
                actual: actual.Words);
        }

        [NamedFact]
        public void TextSplitTests_Text_with_Digit_Expect_without_Digit()
        {
            const string text = "Draußen ist es schön 1";
            TextSplitResult actual = _systemUnderTest.Split(text: text);

            List<string> expected = new List<string> { "Draußen", "ist", "es", "schön" };

            Assert.Equal(
                expected: expected,
                actual: actual.Words);
        }
    }
}
