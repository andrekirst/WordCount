using FluentAssertions;
using WordCount.Models.Results;
using Xunit;

namespace WordCount.Tests.Models;

/// <summary>
/// Unit tests for the LanguageDetectionResult model class.
/// </summary>
public class LanguageDetectionResultTests
{
    #region Default Constructor Tests

    [Fact]
    public void DefaultConstructor_ShouldSetDefaultValues()
    {
        // Act
        var result = new LanguageDetectionResult();

        // Assert
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
        result.IsAutoDetected.Should().BeFalse();
    }

    #endregion

    #region Parameterized Constructor Tests

    [Fact]
    public void Constructor_WithLanguageAndCharacterBased_ShouldSetProperties()
    {
        // Act
        var result = new LanguageDetectionResult("zh", true);

        // Assert
        result.DetectedLanguage.Should().Be("zh");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Theory]
    [InlineData("en", false)]
    [InlineData("de", false)]
    [InlineData("zh", true)]
    [InlineData("ja", true)]
    [InlineData("ko", true)]
    public void Constructor_VariousLanguages_ShouldSetCorrectly(
        string language, bool isCharacterBased)
    {
        // Act
        var result = new LanguageDetectionResult(language, isCharacterBased);

        // Assert
        result.DetectedLanguage.Should().Be(language);
        result.IsCharacterBased.Should().Be(isCharacterBased);
    }

    #endregion

    #region Property Setter Tests

    [Fact]
    public void DetectedLanguage_CanBeModified()
    {
        // Arrange
        var result = new LanguageDetectionResult();

        // Act
        result.DetectedLanguage = "fr";

        // Assert
        result.DetectedLanguage.Should().Be("fr");
    }

    [Fact]
    public void IsCharacterBased_CanBeModified()
    {
        // Arrange
        var result = new LanguageDetectionResult();

        // Act
        result.IsCharacterBased = true;

        // Assert
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void IsAutoDetected_CanBeModified()
    {
        // Arrange
        var result = new LanguageDetectionResult();

        // Act
        result.IsAutoDetected = true;

        // Assert
        result.IsAutoDetected.Should().BeTrue();
    }

    #endregion

    #region Object Initializer Tests

    [Fact]
    public void ObjectInitializer_ShouldAllowSettingAllProperties()
    {
        // Act
        var result = new LanguageDetectionResult
        {
            DetectedLanguage = "ja",
            IsCharacterBased = true,
            IsAutoDetected = true
        };

        // Assert
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
        result.IsAutoDetected.Should().BeTrue();
    }

    [Fact]
    public void ConstructorWithInitializer_ShouldAllowOverridingIsAutoDetected()
    {
        // Act
        var result = new LanguageDetectionResult("zh", true)
        {
            IsAutoDetected = true
        };

        // Assert
        result.DetectedLanguage.Should().Be("zh");
        result.IsCharacterBased.Should().BeTrue();
        result.IsAutoDetected.Should().BeTrue();
    }

    #endregion
}
