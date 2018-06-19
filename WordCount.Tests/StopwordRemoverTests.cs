using Autofac;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models;
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

        [Fact]
        public void StopwordRemoverTests_Analyze_Stopword_a_Mary_has_a_little_lamb_Expect_4_Words_with_Stopword_a()
        {
            List<string> values = new List<string>() { "Mary", "had", "a", "little", "lamb" };

            _mockStopwordLoader
                .Setup(m => m.GetStopwords())
                .Returns(value: new List<string>() { "a" });

            StopwordRemoverResult actual = _systemUnderTest.RemoveStopwords(values: values);

            List<string> expected = new List<string>() { "Mary", "had", "little", "lamb" };

            Assert.NotNull(@object: actual);
            Assert.Equal(expected: expected, actual: actual.Values);
        }

        [Fact]
        public void StopwordRemoverTests_Analyze_Stopword_a_Mary_has_a_little_lamb_Expect_5_Words_without_Stopwords()
        {
            List<string> values = new List<string>() { "Mary", "had", "a", "little", "lamb" };

            _mockStopwordLoader
                .Setup(m => m.GetStopwords())
                .Returns(new List<string>());

            List<string> expected = new List<string>() { "Mary", "had", "a", "little", "lamb" };

            StopwordRemoverResult actual = _systemUnderTest.RemoveStopwords(values: values);

            Assert.NotNull(actual);
            Assert.NotNull(@object: actual.Values);
            Assert.NotEmpty(collection: actual.Values);
            Assert.Equal(expected: expected, actual: actual.Values);
        }
    }
}
