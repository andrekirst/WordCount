﻿using System;
using WordCount.Implementations;
using WordCount.Models;
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

        [Fact]
        public void TextSplitTests_Analyze_Text_Bla_bla_Expect_2_Words()
        {
            TextSplitResult actual = _systemUnderTest.Split(text: "Bla bla");

            Assert.Equal(expected: 2, actual: actual.Values.Count);
        }

        [Fact]
        public void TextSplitTests_Analyze_Stopword_a_This_is_a_Text_Expect_4_Words()
        {
            TextSplitResult actual = _systemUnderTest.Split(
                text: "This is a Text");

            Assert.Equal(expected: 4, actual: actual.Values.Count);
        }

        [Fact]
        public void TextSplitTests_Analyze_Stopword_a_Mary_has_a_little_lamb_with_Newlines_Expect_5_Words()
        {
            TextSplitResult actual = _systemUnderTest.Split(
                text: $"Mary had{Environment.NewLine}a little{Environment.NewLine}lamb");

            Assert.Equal(expected: 5, actual: actual.Values.Count);
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
            Assert.NotNull(@object: actual.Values);
            Assert.Empty(collection: actual.Values);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TextSplitTests_empty_strings_Expect_ValuesAvailable_false(string inputtext)
        {
            TextSplitResult actual = _systemUnderTest.Split(text: inputtext);

            Assert.False(condition: actual.ValuesAvailable);
        }
    }
}