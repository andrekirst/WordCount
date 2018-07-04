using System;
using Autofac;
using Moq;
using System.IO.Abstractions;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class TextFileLoaderTests
    {
        private readonly Mock<IFileSystem> _mockFileSystem;
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly TextFileLoader _systemUnderTest;

        public TextFileLoaderTests()
        {
            _mockFileSystem = new Mock<IFileSystem>();
            _mockDisplayOutput = new Mock<IDisplayOutput>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterInstance(_mockFileSystem.Object)
                .As<IFileSystem>();

            containerBuilder
                .RegisterInstance(_mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterType<TextFileLoader>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<TextFileLoader>();
        }

        [NamedFact]
        public void TextFileLoaderTests_FileNotExist_True_Expect_Output_and_return_empty_string()
        {
            _mockFileSystem
                .Setup(m => m.File.Exists(It.IsAny<string>()))
                .Returns(false);

            string actual = _systemUnderTest.ReadTextFile("datei1.txt");

            Assert.Equal(
                expected: string.Empty,
                actual: actual);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteErrorLine($"File \"datei1.txt\" not found."),
                    times: Times.Once);
        }

        [NamedFact]
        public void TextFileLoaderTests_ReadAllText_Bla_Expect_Bla()
        {
            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllText(It.IsAny<string>()))
                .Returns(value: "Bla");

            _mockFileSystem
                .Setup(m => m.File.Exists(It.IsAny<string>()))
                .Returns(true);

            string actual = _systemUnderTest.ReadTextFile(path: It.IsAny<string>());

            Assert.Equal(expected: "Bla", actual: actual);
        }

        [NamedFact]
        public void TextFileLoaderTests_Text_mit_neuer_Zeile_und_Bindestrich_zu_Text_ohne_Bindestrich_und_vollem_Wort()
        {
            string inputText = $"Das ist ein lan-{Environment.NewLine}ger Text";

            _mockFileSystem
                .Setup(m => m.File.Exists(It.IsAny<string>()))
                .Returns(true);

            _mockFileSystem
                .Setup(m => m.File.ReadAllText(It.IsAny<string>()))
                .Returns(inputText);

            string actual = _systemUnderTest.ReadTextFile(It.IsAny<string>());

            string expected = "Das ist ein langer Text";

            Assert.Equal(
                expected: expected,
                actual: actual);
        }
    }
}
