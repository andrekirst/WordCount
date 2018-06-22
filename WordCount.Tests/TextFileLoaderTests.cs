using Autofac;
using Moq;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Implementations;
using WordCount.Interfaces;
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

        [Fact]
        public void TextFileLoaderTests_FileNotFoundException_Thrown_Expect_DisplayOutput_WriteError()
        {
            _mockFileSystem
                .Setup(m => m.File.ReadAllText("datei1.txt"))
                .Throws(new FileNotFoundException("Error-Message", "datei1.txt"));

            Assert.Throws<FileNotFoundException>(testCode: () =>
            {
                _systemUnderTest.ReadTextFile("datei1.txt");
            });

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteErrorLine($"File \"datei1.txt\" not found."),
                    times: Times.Once);
        }

        [Fact]
        public void TextFileLoaderTests_FileNotFoundException_Thrown_Expect_FileNotFoundException()
        {
            _mockFileSystem
                .Setup(m => m.File.ReadAllText(It.IsAny<string>()))
                .Throws(new FileNotFoundException(It.IsAny<string>(), "datei1.txt"));

            Assert.Throws<FileNotFoundException>(
                testCode: () =>
                {
                    _systemUnderTest.ReadTextFile("datei1.txt");
                });
        }

        [Fact]
        public void TextFileLoaderTests_ReadAllText_Bla_Expect_Bla()
        {
            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllText(It.IsAny<string>()))
                .Returns(value: "Bla");

            string actual = _systemUnderTest.ReadTextFile(path: It.IsAny<string>());

            Assert.Equal(expected: "Bla", actual: actual);
        }
    }
}
