using Autofac;
using Moq;
using System.Collections.Generic;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class StopwordRemoverTests
    {
        private Mock<IStopwordLoader> _mockStopwordLoader;
        private readonly StopwordRemover _systemUnderTest;

        public StopwordRemoverTests()
        {
            _mockStopwordLoader = new Mock<IStopwordLoader>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(_mockStopwordLoader.Object)
                .As<IStopwordLoader>()
                .SingleInstance();

            containerBuilder
                .RegisterType<StopwordRemover>()
                .SingleInstance();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<StopwordRemover>();
        }

        [NamedFact]
        public void StopwordRemoverTests_Analyze_Stopword_a_Mary_has_a_little_lamb_Expect_4_Words_with_Stopword_a()
        {
            List<string> values = new List<string>() { "Mary", "had", "a", "little", "lamb" };

            _mockStopwordLoader
                .Setup(m => m.GetStopwords())
                .Returns(value: new List<string>() { "a" });

            StopwordRemoverResult actual = _systemUnderTest.RemoveStopwords(words: values);

            List<string> expected = new List<string>() { "Mary", "had", "little", "lamb" };

            Assert.NotNull(@object: actual);
            Assert.Equal(expected: expected, actual: actual.Words);
        }

        [NamedFact]
        public void StopwordRemoverTests_Analyze_Stopword_a_Mary_has_a_little_lamb_Expect_5_Words_without_Stopwords()
        {
            List<string> values = new List<string>() { "Mary", "had", "a", "little", "lamb" };

            _mockStopwordLoader
                .Setup(m => m.GetStopwords())
                .Returns(new List<string>());

            List<string> expected = new List<string>() { "Mary", "had", "a", "little", "lamb" };

            StopwordRemoverResult actual = _systemUnderTest.RemoveStopwords(words: values);

            Assert.NotNull(actual);
            Assert.NotNull(@object: actual.Words);
            Assert.NotEmpty(collection: actual.Words);
            Assert.Equal(expected: expected, actual: actual.Words);
        }
    }
}
