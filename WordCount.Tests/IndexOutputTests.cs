using System.Collections.Generic;
using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Models.Requests;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class IndexOutputTests
    {
        /*
        private readonly IIndexParameterParser _indexParameterParser;
        private readonly IDictionaryParameterParser _dictionaryParameterParser;
         */

        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<IDictionaryFileLoader> _mockDictionaryFileLoader;
        private readonly Mock<IIndexParameterParser> _mockIndexParameterParser;
        private readonly Mock<IDictionaryParameterParser> _mockDictionaryParameterParser;
        private readonly IndexOutput _systemUnderTest;

        public IndexOutputTests()
        {
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockDictionaryFileLoader = new Mock<IDictionaryFileLoader>();
            _mockDictionaryParameterParser = new Mock<IDictionaryParameterParser>();
            _mockIndexParameterParser = new Mock<IIndexParameterParser>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockDictionaryFileLoader.Object)
                .As<IDictionaryFileLoader>();

            containerBuilder
                .RegisterInstance(instance: _mockIndexParameterParser.Object)
                .As<IIndexParameterParser>();

            containerBuilder
                .RegisterInstance(instance: _mockDictionaryParameterParser.Object)
                .As<IDictionaryParameterParser>();

            containerBuilder
                .RegisterType<IndexOutput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<IndexOutput>();
        }

        [NamedFact]
        public void IndexOutputTests_DistinctWords_Bla_bla_Expect_Output_Index_bla_Bla()
        {
            List<string> verifyList = new List<string>();

            _mockIndexParameterParser
                .Setup(m => m.ParseIndexParameter())
                .Returns(new IndexParameter {IsPresent = true});

            _mockDictionaryParameterParser
                .Setup(m => m.ParseDictionaryParameter())
                .Returns(new DictionaryParameter {IsPresent = false});

            _mockDisplayOutput
                .Setup(expression: m => m.WriteLine(It.IsAny<string>()))
                .Callback<string>(action: (s) => verifyList.Add(item: s));

            IndexOutputRequest indexOutputRequest = new IndexOutputRequest
            {
                DistinctWords = new List<string> { "Bla", "bla" }
            };

            _systemUnderTest.OutputIndex(indexOutputRequest: indexOutputRequest);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteResourceLine("INDEX"),
                    times: Times.Once);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("bla"),
                    times: Times.Once);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("Bla"),
                    times: Times.Once);

            Assert.Equal(expected: "bla", actual: verifyList[index: 0]);
            Assert.Equal(expected: "Bla", actual: verifyList[index: 1]);
        }

        [NamedFact]
        public void IndexOutputTests_DistinctWords_Bla_bla_Expect_Output_Index_With_Dict_bla_Bla_star()
        {
            List<string> verifyList = new List<string>();

            _mockDictionaryParameterParser
                .Setup(m => m.ParseDictionaryParameter())
                .Returns(new DictionaryParameter
                {
                    IsPresent = true,
                    FileName = It.IsAny<string>()
                });

            _mockIndexParameterParser
                .Setup(m => m.ParseIndexParameter())
                .Returns(new IndexParameter {IsPresent = true});

            _mockDisplayOutput
                .Setup(expression: m => m.WriteLine(It.IsAny<string>()))
                .Callback<string>(action: (s) => verifyList.Add(item: s));

            _mockDictionaryFileLoader
                .Setup(expression: m => m.ReadWords())
                .Returns(value: new List<string> { "bla" });

            IndexOutputRequest indexOutputRequest = new IndexOutputRequest
            {
                DistinctWords = new List<string> { "Bla", "bla" }
            };

            _systemUnderTest.OutputIndex(indexOutputRequest: indexOutputRequest);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteResourceLine("INDEX_WITH_UNKNOWN", 1),
                    times: Times.Once);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("bla"),
                    times: Times.Once);

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine("Bla*"),
                    times: Times.Once);

            Assert.Equal(expected: "bla", actual: verifyList[index: 0]);
            Assert.Equal(expected: "Bla*", actual: verifyList[index: 1]);
        }
    }
}
