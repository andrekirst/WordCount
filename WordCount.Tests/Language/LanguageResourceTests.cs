using Autofac;
using Moq;
using WordCount.Abstractions.SystemAbstractions.Globalization;
using WordCount.Abstractions.SystemAbstractions.Resources;
using WordCount.Implementations.Language;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.Language
{
    public class LanguageResourceTests
    {
        private readonly Mock<ILanguageDecision> _mockLanguageDecision;
        private readonly Mock<ICultureInfo> _mockCultureInfo;
        private readonly Mock<IResourceManager> _mockResourceManager;
        private readonly LanguageResource _systemUnderTest;

        public LanguageResourceTests()
        {
            _mockCultureInfo = new Mock<ICultureInfo>();
            _mockLanguageDecision = new Mock<ILanguageDecision>();
            _mockResourceManager = new Mock<IResourceManager>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockCultureInfo.Object)
                .As<ICultureInfo>();

            containerBuilder
                .RegisterInstance(instance: _mockLanguageDecision.Object)
                .As<ILanguageDecision>();

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
        public void LanguageRessourceTests_languagedecision_is_de_Expect_de_content()
        {
            _mockLanguageDecision
                .Setup(m => m.DecideLanguage())
                .Returns(value: new DecideLanguageResult
                {
                    Language = "de"
                });

            _systemUnderTest.GetResourceStringById(resourceIdent: "INDEX");

            _mockCultureInfo
                .Verify(v => v.GetCultureInfo("de-DE"), Times.Once);
        }

        [NamedFact]
        public void LanguageRessourceTests_languagedecision_is_de_Expect_de_content_with_value()
        {
            _mockResourceManager
                .Setup(m => m.GetString("RESOURCEKEY", It.IsAny<System.Globalization.CultureInfo>()))
                .Returns(value: "Deutscher Text");

            _mockLanguageDecision
                .Setup(m => m.DecideLanguage())
                .Returns(value: new DecideLanguageResult
                {
                    Language = "de"
                });

            string actual = _systemUnderTest.GetResourceStringById(resourceIdent: "RESOURCEKEY");

            Assert.Equal(expected: "Deutscher Text", actual: actual);

            _mockCultureInfo
                .Verify(v => v.GetCultureInfo("de-DE"), Times.Once);
        }
    }
}
