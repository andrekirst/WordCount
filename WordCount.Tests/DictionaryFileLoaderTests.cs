using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class DictionaryFileLoaderTests
    {
        private readonly Mock<IFileSystem> _mockFileSystem;
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<IDictionaryParameterParser> _mockDictionaryParameterParser;
        private readonly DictionaryFileLoader _systemUnderTest;

        public DictionaryFileLoaderTests()
        {
            _mockFileSystem = new Mock<IFileSystem>();
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockDictionaryParameterParser = new Mock<IDictionaryParameterParser>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockFileSystem.Object)
                .As<IFileSystem>();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockDictionaryParameterParser.Object)
                .As<IDictionaryParameterParser>();

            containerBuilder
                .RegisterType<DictionaryFileLoader>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<DictionaryFileLoader>();
        }

        [NamedFact]
        public void DictionaryFileLoaderTests_FileNotFound_Expect_Empty_List_And_DisplayOutput_WriteErrorLine()
        {
            _mockDictionaryParameterParser
                .Setup(m => m.ParseDictionaryParameter())
                .Returns(new DictionaryParameter
                {
                    IsPresent = true,
                    FileName = "datei_gibt_es_nicht.txt"
                });

            _mockFileSystem
                .Setup(expression: m => m.File.Exists("datei_gibt_es_nicht.txt"))
                .Returns(value: false);

            List<string> actual = _systemUnderTest.ReadWords();

            Assert.NotNull(@object: actual);
            Assert.Empty(collection: actual);
            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteErrorLine("File \"datei_gibt_es_nicht.txt\" not found."),
                    times: Times.Once);
        }

        [NamedFact]
        public void DictionaryFileLoaderTests_File_Content_WordA_WordB_Expect_WordA_WordB()
        {
            _mockDictionaryParameterParser
                .Setup(m => m.ParseDictionaryParameter())
                .Returns(new DictionaryParameter
                {
                    IsPresent = true,
                    FileName = It.IsAny<string>()
                });

            _mockFileSystem
                .Setup(expression: m => m.File.Exists(It.IsAny<string>()))
                .Returns(value: true);

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Returns(value: new[] {"WordA", "WordB"});

            List<string> expected = new List<string> { "WordA", "WordB" };
            List<string> actual = _systemUnderTest.ReadWords();

            Assert.Equal(expected: expected, actual: actual);
        }

        [NamedFact]
        public void DictionaryFileLoaderTests_File_Content_Null_Expect_Empty()
        {
            _mockDictionaryParameterParser
                .Setup(m => m.ParseDictionaryParameter())
                .Returns(new DictionaryParameter
                {
                    IsPresent = true,
                    FileName = It.IsAny<string>()
                });

            _mockFileSystem
                .Setup(expression: m => m.File.Exists(It.IsAny<string>()))
                .Returns(value: true);

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Returns(value: null);

            List<string> actual = _systemUnderTest.ReadWords();

            Assert.NotNull(@object: actual);
            Assert.Empty(collection: actual);
        }

        [NamedFact]
        public void DictionaryFileLoaderTests_File_Not_Exists_Expect_Empty()
        {
            _mockDictionaryParameterParser
                .Setup(m => m.ParseDictionaryParameter())
                .Returns(new DictionaryParameter
                {
                    IsPresent = true,
                    FileName = It.IsAny<string>()
                });

            _mockFileSystem
                .Setup(expression: m => m.File.Exists(It.IsAny<string>()))
                .Returns(value: false);

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Throws<FileNotFoundException>();
            List<string> actual = _systemUnderTest.ReadWords();

            Assert.NotNull(@object: actual);
            Assert.Empty(collection: actual);
        }

        [NamedFact]
        public void DictionaryFileLoaderTests_DictionaryParameter_is_not_present_do_not_call_File_ReadAllLines()
        {
            _mockDictionaryParameterParser
                .Setup(m => m.ParseDictionaryParameter())
                .Returns(new DictionaryParameter { IsPresent = false });

            _systemUnderTest.ReadWords();

            _mockFileSystem
                .Verify(
                    expression: v => v.File.ReadAllLines(null),
                    times: Times.Never);
        }
    }
}
