using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Abstractions.SystemAbstractions.Globalization;
using WordCount.Abstractions.SystemAbstractions.Resources;
using WordCount.Implementations.Language;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;
using Xunit;

namespace WordCount.Tests.Language;

public class LanguageResourceTests
{
    //[Theory, AutoMoqData]
    //public void LanguageRessourceTests_languagedecision_is_de_Expect_de_content(
    //    [Frozen] Mock<ILanguageDecision> languageDecision,
    //    [Frozen] Mock<ICultureInfo> cultureInfo,
    //    LanguageResource sut)
    //{
    //    languageDecision
    //        .Setup(m => m.DecideLanguage())
    //        .Returns(new DecideLanguageResult
    //        {
    //            Language = "de"
    //        });

    //    sut.GetResourceStringById("INDEX");

    //    cultureInfo
    //        .Verify(v => v.GetCultureInfo("de-DE"), Times.Once);
    //}

    //[Theory, AutoMoqData]
    //public void LanguageRessourceTests_languagedecision_is_de_Expect_de_content_with_value(
    //    [Frozen] Mock<IResourceManager> resourceManager,
    //    [Frozen] Mock<ILanguageDecision> languageDecision,
    //    [Frozen] Mock<ICultureInfo> cultureInfo,
    //    LanguageResource sut)
    //{
    //    resourceManager
    //        .Setup(m => m.GetString("RESOURCEKEY", It.IsAny<System.Globalization.CultureInfo>()))
    //        .Returns("Deutscher Text");

    //    languageDecision
    //        .Setup(m => m.DecideLanguage())
    //        .Returns(new DecideLanguageResult
    //        {
    //            Language = "de"
    //        });

    //    var actual = sut.GetResourceStringById("RESOURCEKEY");

    //    actual.Should().Be("Deutscher Text");

    //    cultureInfo.Verify(v => v.GetCultureInfo("de-DE"), Times.Once);
    //}

    //[Theory, AutoMoqData]
    //public void LanguageRessourceTests_DetectLongestResourceString_Langer_Text_und_kurzer_Text_Expect_Laenge_des_laengerem(
    //    [Frozen] Mock<ILanguageDecision> languageDecision,
    //    [Frozen] Mock<IResourceManager> resourceManager,
    //    LanguageResource sut)
    //{
    //    resourceManager
    //        .Setup(m => m.GetString("KEY1", It.IsAny<System.Globalization.CultureInfo>()))
    //        .Returns("Kurzer Text");

    //    resourceManager
    //        .Setup(m => m.GetString("KEY2", It.IsAny<System.Globalization.CultureInfo>()))
    //        .Returns("Langer Text mit viel Inhalt");

    //    languageDecision
    //        .Setup(m => m.DecideLanguage())
    //        .Returns(new DecideLanguageResult
    //        {
    //            Language = "de"
    //        });

    //    var actual = sut.DetectLongestResourceString(new[] { "KEY1", "KEY2" });

    //    actual.Should().Be(27);
    //}
}