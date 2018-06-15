﻿using Autofac;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using WordCount.Implementations;
using Xunit;

namespace WordCount.Tests
{
    public class StopwordLoaderTests
    {
        private readonly Mock<IFileSystem> _mockFileSystem;
        private readonly StopwordLoader _systemUnderTest;

        public StopwordLoaderTests()
        {
            _mockFileSystem = new Mock<IFileSystem>();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterInstance(instance: _mockFileSystem.Object)
                .As<IFileSystem>();

            containerBuilder
                .RegisterType<StopwordLoader>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<StopwordLoader>();
        }

        [Fact]
        public void StopwordLoaderTests_GetStopwords_FileNotExist_Return_null()
        {
            _mockFileSystem
                .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
                .Throws(new FileNotFoundException());

            List<string> actual = _systemUnderTest.GetStopwords();

            Assert.Null(@object: actual);
        }

        [Fact]
        public void StopwordLoaderTests_GetStopwords_FileEmpty_Return_null()
        {
            _mockFileSystem
                .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
                .Returns(new string[] { });

            List<string> actual = _systemUnderTest.GetStopwords();

            Assert.Null(@object: actual);
        }

        [Fact]
        public void StopwordLoaderTests_GetStopwords_Contains_1_Row_With_word_a_Return_List_with_one_Entry_a()
        {
            _mockFileSystem
                .Setup(m => m.File.Exists(It.IsAny<string>()))
                .Returns(true);

            _mockFileSystem
                .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
                .Returns(new[] { "a" });

            List<string> actual = _systemUnderTest.GetStopwords();

            List<string> expected = new List<string>()
            {
                "a"
            };

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}