using FluentAssertions;
using WordCount.Implementations.TextSplitting;
using WordCount.Interfaces;
using Xunit;

namespace WordCount.Tests.TextSplitting;

/// <summary>
/// Unit tests for the CharacterBasedTextSplitStrategy class that handles
/// text splitting for character-based languages like Chinese, Japanese, and Korean.
/// </summary>
public class CharacterBasedTextSplitStrategyTests
{
    private readonly ITextSplitStrategy _sut;

    public CharacterBasedTextSplitStrategyTests()
    {
        _sut = new CharacterBasedTextSplitStrategy();
    }

    #region Chinese Text Splitting

    [Fact]
    public void SplitText_ChineseText_ShouldReturnEachCharacterAsWord()
    {
        // Arrange - "Hello World" in Chinese
        const string text = "你好世界";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().ContainInOrder("你", "好", "世", "界");
    }

    [Fact]
    public void SplitText_ChineseTextWithSpaces_ShouldIgnoreSpaces()
    {
        // Arrange
        const string text = "你好 世界";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().ContainInOrder("你", "好", "世", "界");
    }

    [Fact]
    public void SplitText_ChineseTextWithPunctuation_ShouldIgnorePunctuation()
    {
        // Arrange - Chinese text with Chinese punctuation marks
        const string text = "你好！世界。";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().ContainInOrder("你", "好", "世", "界");
    }

    [Fact]
    public void SplitText_LongerChineseText_ShouldReturnAllCharacters()
    {
        // Arrange - "I love programming" in Chinese
        const string text = "我爱编程";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().ContainInOrder("我", "爱", "编", "程");
    }

    #endregion

    #region Japanese Text Splitting

    [Fact]
    public void SplitText_JapaneseHiragana_ShouldReturnEachCharacterAsWord()
    {
        // Arrange - "Konnichiwa" in Hiragana
        const string text = "こんにちは";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(5);
        result.Should().ContainInOrder("こ", "ん", "に", "ち", "は");
    }

    [Fact]
    public void SplitText_JapaneseKatakana_ShouldReturnEachCharacterAsWord()
    {
        // Arrange - "Computer" in Katakana
        const string text = "コンピュータ";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(6);
        result.Should().ContainInOrder("コ", "ン", "ピ", "ュ", "ー", "タ");
    }

    [Fact]
    public void SplitText_JapaneseMixedKanaAndKanji_ShouldReturnAllCharacters()
    {
        // Arrange - Mixed Japanese text
        const string text = "日本語";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(3);
        result.Should().ContainInOrder("日", "本", "語");
    }

    #endregion

    #region Korean Text Splitting

    [Fact]
    public void SplitText_KoreanText_ShouldReturnEachCharacterAsWord()
    {
        // Arrange - "Hello" in Korean
        const string text = "안녕";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainInOrder("안", "녕");
    }

    [Fact]
    public void SplitText_KoreanSentence_ShouldReturnAllCharacters()
    {
        // Arrange - "I love you" in Korean
        const string text = "사랑해요";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().ContainInOrder("사", "랑", "해", "요");
    }

    #endregion

    #region Mixed CJK and Latin Text

    [Fact]
    public void SplitText_MixedChineseAndEnglish_ShouldHandleBothCorrectly()
    {
        // Arrange - "Hello" + Chinese "World"
        const string text = "Hello 世界";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain("Hello");
        result.Should().Contain("世");
        result.Should().Contain("界");
    }

    [Fact]
    public void SplitText_MixedJapaneseAndEnglish_ShouldHandleBothCorrectly()
    {
        // Arrange - English + Japanese Hiragana
        const string text = "Hello こんにちは";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(6);
        result.Should().Contain("Hello");
        result.Should().ContainInOrder("こ", "ん", "に", "ち", "は");
    }

    [Fact]
    public void SplitText_ComplexMixedText_ShouldPreserveLatinWords()
    {
        // Arrange
        const string text = "Programming 编程 is fun";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().Contain("Programming");
        result.Should().Contain("编");
        result.Should().Contain("程");
        result.Should().Contain("is");
        result.Should().Contain("fun");
    }

    [Fact]
    public void SplitText_MixedWithHyphenatedEnglish_ShouldPreserveHyphens()
    {
        // Arrange - Hyphenated word + Chinese
        const string text = "pre-condition 世界";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().Contain("pre-condition");
        result.Should().Contain("世");
        result.Should().Contain("界");
    }

    [Fact]
    public void SplitText_MixedWithApostrophe_ShouldPreserveApostrophe()
    {
        // Arrange - Possessive + Chinese
        const string text = "Hamlet's 世界";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().Contain("Hamlet's");
        result.Should().Contain("世");
        result.Should().Contain("界");
    }

    #endregion

    #region Edge Cases

    [Theory]
    [InlineData(null)]
    [InlineData("")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public void SplitText_NullOrEmptyText_ShouldReturnEmptyList(string? text)
#pragma warning restore CS8632
    {
        // Act
        var result = _sut.SplitText(text!);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void SplitText_WhitespaceOnly_ShouldReturnEmptyList()
    {
        // Arrange
        const string text = "   \t\n   ";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void SplitText_SingleCjkCharacter_ShouldReturnSingleWord()
    {
        // Arrange
        const string text = "中";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(1);
        result.Should().Contain("中");
    }

    [Fact]
    public void SplitText_SingleLatinWord_ShouldReturnSingleWord()
    {
        // Arrange
        const string text = "Hello";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(1);
        result.Should().Contain("Hello");
    }

    [Fact]
    public void SplitText_NumbersOnly_ShouldReturnEmptyList()
    {
        // Arrange - Numbers are typically not treated as words in this strategy
        const string text = "12345";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void SplitText_TrailingHyphen_ShouldRemoveTrailingHyphen()
    {
        // Arrange - Word ending with hyphen (like a line break continuation)
        const string text = "pre- 世界";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().Contain("pre");
        result.Should().NotContain("pre-");
    }

    #endregion

    #region Unicode Edge Cases

    [Fact]
    public void SplitText_ChineseExtensionA_ShouldBeTreatedAsCjk()
    {
        // Arrange - CJK Extension A character (less common)
        const string text = "㐀";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(1);
    }

    [Fact]
    public void SplitText_FullWidthLatinLetters_ShouldNotBeTreatedAsCjk()
    {
        // Arrange - Full-width Latin letters (commonly used in Japanese text)
        const string text = "ＡＢＣ";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        // Full-width Latin should be handled as Latin characters (grouped into words)
        result.Should().HaveCount(1);
    }

    #endregion
}
