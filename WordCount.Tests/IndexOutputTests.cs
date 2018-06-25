using System.Collections.Generic;
using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class IndexOutputTests
    {
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<IDictionaryFileLoader> _mockDictionaryFileLoader;
        private readonly IndexOutput _systemUnderTest;

        public IndexOutputTests()
        {
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockDictionaryFileLoader = new Mock<IDictionaryFileLoader>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockDictionaryFileLoader.Object)
                .As<IDictionaryFileLoader>();

            containerBuilder
                .RegisterType<IndexOutput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<IndexOutput>();
        }

        [Fact]
        public void IndexOutputTests_DistinctWords_Bla_bla_Expect_Output_Index_bla_Bla()
        {
            List<string> verifyList = new List<string>();

            _mockDisplayOutput
                .Setup(expression: m => m.WriteLine(It.IsAny<string>()))
                .Callback<string>(action: (s) => verifyList.Add(item: s));

            IndexOutputRequest indexOutputRequest = new IndexOutputRequest()
            {
                DistinctWords = new List<string>() { "Bla", "bla" }
            };

            _systemUnderTest.OutputIndex(indexOutputRequest: indexOutputRequest);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("Index:"),
                    times: Times.Once);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("bla"),
                    times: Times.Once);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("Bla"),
                    times: Times.Once);

            Assert.Equal(expected: "Index:", actual: verifyList[index: 0]);
            Assert.Equal(expected: "bla", actual: verifyList[index: 1]);
            Assert.Equal(expected: "Bla", actual: verifyList[index: 2]);
        }

        [Fact]
        public void IndexOutputTests_DistinctWords_Bla_bla_Expect_Output_Index_With_Dict_bla_Bla_star()
        {
            List<string> verifyList = new List<string>();

            _mockDisplayOutput
                .Setup(expression: m => m.WriteLine(It.IsAny<string>()))
                .Callback<string>(action: (s) => verifyList.Add(item: s));

            _mockDictionaryFileLoader
                .Setup(expression: m => m.ReadWords("datei.txt"))
                .Returns(value: new List<string>() { "bla" });

            IndexOutputRequest indexOutputRequest = new IndexOutputRequest()
            {
                DistinctWords = new List<string>() { "Bla", "bla" },
                DictionaryTextFile = "datei.txt",
                IsDictionaryParameterPresent = true
            };

            _systemUnderTest.OutputIndex(indexOutputRequest: indexOutputRequest);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("Index: (unknown: 1)"),
                    times: Times.Once);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("bla"),
                    times: Times.Once);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("Bla*"),
                    times: Times.Once);

            Assert.Equal(expected: "Index: (unknown: 1)", actual: verifyList[index: 0]);
            Assert.Equal(expected: "bla", actual: verifyList[index: 1]);
            Assert.Equal(expected: "Bla*", actual: verifyList[index: 2]);
        }
    }
}
