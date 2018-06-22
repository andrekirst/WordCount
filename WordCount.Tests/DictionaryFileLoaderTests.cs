using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using Autofac;
using Moq;
using WordCount.Implementations;
using Xunit;

namespace WordCount.Tests
{
    public class DictionaryFileLoaderTests
    {
        private Mock<IFileSystem> _mockFileSystem;
        private readonly DictionaryFileLoader _systemUnderTest;

        public DictionaryFileLoaderTests()
        {
            _mockFileSystem = new Mock<IFileSystem>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockFileSystem.Object)
                .As<IFileSystem>();

            containerBuilder
                .RegisterType<DictionaryFileLoader>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<DictionaryFileLoader>();
        }

        [Fact]
        public void DictionaryFileLoaderTests_File_Content_WordA_WordB_Expect_WordA_WordB()
        {
            _mockFileSystem
                .Setup(expression: m => m.File.Exists(It.IsAny<string>()))
                .Returns(value: true);

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Returns(value: new[] {"WordA", "WordB"});

            List<string> expected = new List<string>() { "WordA", "WordB" };
            List<string> actual = _systemUnderTest.ReadWords(path: It.IsAny<string>());

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void DictionaryFileLoaderTests_File_Content_Null_Expect_Empty()
        {
            _mockFileSystem
                .Setup(expression: m => m.File.Exists(It.IsAny<string>()))
                .Returns(value: true);

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Returns(value: null);
            List<string> actual = _systemUnderTest.ReadWords(path: It.IsAny<string>());

            Assert.NotNull(@object: actual);
            Assert.Empty(collection: actual);
        }

        [Fact]
        public void DictionaryFileLoaderTests_File_Not_Exists_Expect_Empty()
        {
            _mockFileSystem
                .Setup(expression: m => m.File.Exists(It.IsAny<string>()))
                .Returns(value: false);

            _mockFileSystem
                .Setup(expression: m => m.File.ReadAllLines(It.IsAny<string>()))
                .Throws<FileNotFoundException>();
            List<string> actual = _systemUnderTest.ReadWords(path: It.IsAny<string>());

            Assert.NotNull(@object: actual);
            Assert.Empty(collection: actual);
        }
    }
}
