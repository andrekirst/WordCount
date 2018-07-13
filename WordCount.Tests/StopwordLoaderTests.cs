using Autofac;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class StopwordLoaderTests
    {
        private readonly Mock<IFileSystem> _mockFileSystem;
        private readonly Mock<IStopwordListParameterParser> _mockStopwordListParameterParser;
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<ILanguageParameterParser> _mockLanguageParameterParser;
        private readonly StopwordLoader _systemUnderTest;

        public StopwordLoaderTests()
        {
            _mockFileSystem = new Mock<IFileSystem>();
            _mockStopwordListParameterParser = new Mock<IStopwordListParameterParser>();
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockLanguageParameterParser = new Mock<ILanguageParameterParser>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockFileSystem.Object)
                .As<IFileSystem>();

            containerBuilder
                .RegisterInstance(instance: _mockStopwordListParameterParser.Object)
                .As<IStopwordListParameterParser>();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockLanguageParameterParser.Object)
                .As<ILanguageParameterParser>();

            containerBuilder
                .RegisterType<StopwordLoader>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<StopwordLoader>();
        }

        [NamedFact]
        public void StopwordLoaderTests_GetStopwords_FileNotExist_Return_EmptyList()
        {
            _mockLanguageParameterParser
                .Setup(m => m.ParseLanguageParameter())
                .Returns(new LanguageParameter {Language = "en"});

            _mockStopwordListParameterParser
                .Setup(m => m.ParseStopwordListParameter())
                .Returns(new StopwordListParameter() { IsPresent = false });

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Throws(exception: new FileNotFoundException());

            List<string> actual = _systemUnderTest.GetStopwords();

            Assert.NotNull(@object: actual);
            Assert.Empty(collection: actual);

            _mockDisplayOutput
                .Verify(v => v.WriteLine(It.IsAny<string>()), Times.Never);
        }

        [NamedFact]
        public void StopwordLoaderTests_GetStopwords_FileEmpty_Return_EmptyList()
        {
            _mockLanguageParameterParser
                .Setup(m => m.ParseLanguageParameter())
                .Returns(new LanguageParameter {Language = "en"});

            _mockStopwordListParameterParser
                .Setup(m => m.ParseStopwordListParameter())
                .Returns(new StopwordListParameter() { IsPresent = false });

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Returns(value: Array.Empty<string>());

            List<string> actual = _systemUnderTest.GetStopwords();

            Assert.NotNull(@object: actual);
            Assert.Empty(collection: actual);

            _mockDisplayOutput
                .Verify(v => v.WriteLine(It.IsAny<string>()), Times.Never);
        }

        [NamedFact]
        public void StopwordLoaderTests_GetStopwords_Contains_1_Row_With_word_a_Return_List_with_one_Entry_a()
        {
            _mockLanguageParameterParser
                .Setup(m => m.ParseLanguageParameter())
                .Returns(new LanguageParameter {Language = "en"});

            _mockStopwordListParameterParser
                .Setup(m => m.ParseStopwordListParameter())
                .Returns(new StopwordListParameter() { IsPresent = false });

            _mockFileSystem
                .Setup(expression: m => m.File.Exists(It.IsAny<string>()))
                .Returns(value: true);

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Returns(value: new[] { "a" });

            List<string> actual = _systemUnderTest.GetStopwords();

            List<string> expected = new List<string>
            {
                "a"
            };

            Assert.Equal(expected: expected, actual: actual);

            _mockDisplayOutput
                .Verify(v => v.WriteLine(It.IsAny<string>()), Times.Never);
        }

        [NamedFact]
        public void StopwordLoaderTests_StopwordParameter_is_present_take_StopwordParameter_FileName()
        {
            _mockStopwordListParameterParser
                .Setup(m => m.ParseStopwordListParameter())
                .Returns(new StopwordListParameter() { IsPresent = true, FileName = "meinestopliste.txt" });

            _mockFileSystem
                .Setup(m => m.File.Exists("meinestopliste.txt"))
                .Returns(true);

            _systemUnderTest.GetStopwords();

            _mockFileSystem
                .Verify(v => v.File.Exists("meinestopliste.txt"), Times.Once);

            _mockFileSystem
                .Verify(v => v.File.ReadAllLines("meinestopliste.txt"), Times.Once);

            _mockDisplayOutput
                .Verify(v => v.WriteResourceLine("USED_STOPWORDLIST", "meinestopliste.txt"),
                    Times.Once);
        }

        [NamedFact]
        public void StopwordLoaderTests_StopwordParameter_is_not_present_language_is_de_expect_loading_de_file()
        {
            _mockStopwordListParameterParser
                .Setup(expression: m => m.ParseStopwordListParameter())
                .Returns(value: new StopwordListParameter { IsPresent = false });

            _mockLanguageParameterParser
                .Setup(m => m.ParseLanguageParameter())
                .Returns(new LanguageParameter {Language = "de"});

            _mockFileSystem
                .Setup(m => m.File.Exists(It.IsAny<string>()))
                .Returns(true);

            _systemUnderTest.GetStopwords();

            _mockFileSystem
                .Verify(v => v.File.Exists("stopwords.de.txt"), Times.Once);

            _mockFileSystem
                .Verify(v => v.File.ReadAllLines("stopwords.de.txt"), Times.Once);
        }
    }
}
