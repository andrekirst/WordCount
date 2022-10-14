using Moq;
using System.Collections.Specialized;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Abstractions.SystemAbstractions.Configuration;
using WordCount.Implementations;
using Xunit;

namespace WordCount.Tests.SystemAbstractionTests.ConfigurationTests;

public class AppSettingsReaderTests
{
    //[Theory, AutoMoqData]
    //public void AppSettingsReaderTests_DefaultLanguage_Expect_key_call_defaultLanguage(
    //    string language,
    //    [Frozen] Mock<IConfigurationManager> configurationManager,
    //    AppSettingsReader sut)
    //{
    //    var nameValueCollection = new NameValueCollection
    //    {
    //        { "defaultLanguage", language }
    //    };
        
    //    configurationManager
    //        .SetupGet(m => m.AppSettings)
    //        .Returns(nameValueCollection);

    //    var actual = sut.DefaultLanguage;

    //    actual.Should().Be(language);

    //    configurationManager.Verify(v => v.AppSettings, Times.Once);
    //}
}