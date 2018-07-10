using System;
using System.Linq;
using Autofac;
using Moq;
using WordCount.Abstractions.CultureInfo;
using WordCount.Abstractions.ResourceManager;
using WordCount.Implementations;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class LanguageResourceTests
    {
        private readonly Mock<ILanguageParameterParser> _mockLanguageParameterParser;
        private readonly Mock<ICultureInfo> _mockCultureInfo;
        private readonly Mock<IResourceManager> _mockResourceManager;
        private readonly LanguageResource _systemUnderTest;

        public LanguageResourceTests()
        {
            _mockCultureInfo = new Mock<ICultureInfo>();
            _mockLanguageParameterParser = new Mock<ILanguageParameterParser>();
            _mockResourceManager = new Mock<IResourceManager>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockCultureInfo.Object)
                .As<ICultureInfo>();

            containerBuilder
                .RegisterInstance(instance: _mockLanguageParameterParser.Object)
                .As<ILanguageParameterParser>();

            containerBuilder
                .RegisterInstance(instance: _mockResourceManager.Object)
                .As<IResourceManager>();

            containerBuilder
                .RegisterType<LanguageResource>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<LanguageResource>();
        }

        [NamedFact]
        public void LanguageRessourceTests_Parameter_is_not_present_Expect_english_content()
        {
            _mockLanguageParameterParser
                .Setup(m => m.ParseLanguageParameter())
                .Returns(value: new LanguageParameter {IsPresent = false});

            _systemUnderTest.GetResourceStringById(resourceIdent: "INDEX");

            _mockCultureInfo
                .Verify(v => v.GetCultureInfo("en-US"), Times.Once);
        }

        [NamedFact]
        public void LanguageRessourceTests_Parameter_is_present_paramater_language_is_de_Expect_de_content()
        {
            _mockLanguageParameterParser
                .Setup(m => m.ParseLanguageParameter())
                .Returns(value: new LanguageParameter
                {
                    IsPresent = true, Language = "de"
                });

            _systemUnderTest.GetResourceStringById(resourceIdent: "INDEX");

            _mockCultureInfo
                .Verify(v => v.GetCultureInfo("de-DE"), Times.Once);
        }

        [NamedFact]
        public void LanguageRessourceTests_Parameter_is_present_paramater_language_is_de_Expect_de_content_with_value()
        {
            _mockResourceManager
                .Setup(m => m.GetString("RESOURCEKEY", It.IsAny<System.Globalization.CultureInfo>()))
                .Returns(value: "Deutscher Text");

            _mockLanguageParameterParser
                .Setup(m => m.ParseLanguageParameter())
                .Returns(value: new LanguageParameter
                {
                    IsPresent = true,
                    Language = "de"
                });

            string actual = _systemUnderTest.GetResourceStringById(resourceIdent: "RESOURCEKEY");

            Assert.Equal(expected: "Deutscher Text", actual: actual);

            _mockCultureInfo
                .Verify(v => v.GetCultureInfo("de-DE"), Times.Once);
        }
    }
}
