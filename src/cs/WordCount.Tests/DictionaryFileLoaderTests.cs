// using System.Collections.Generic;
// using System.IO;
// using System.IO.Abstractions;
// using AutoFixture.Xunit2;
// using FluentAssertions;
// using Moq;
// using WordCount.Implementations;
// using WordCount.Interfaces.ArgumentsHandling;
// using WordCount.Interfaces.Output;
// using WordCount.Models.Parameters;
// using Xunit;

// namespace WordCount.Tests;

// public class DictionaryFileLoaderTests
// {
//     [Theory, AutoMoqData]
//     public void DictionaryFileLoaderTests_FileNotFound_Expect_Empty_List_And_DisplayOutput_WriteErrorLine(
//         [Frozen] Mock<IDictionaryParameterParser> dictionaryParameterParser,
//         [Frozen] Mock<IFileSystem> fileSystem,
//         [Frozen] Mock<IDisplayOutput> displayOutput,
//         DictionaryFileLoader sut)
//     {
//         dictionaryParameterParser
//             .Setup(m => m.ParseDictionaryParameter())
//             .Returns(new DictionaryParameter
//             {
//                 IsPresent = true,
//                 FileName = "datei_gibt_es_nicht.txt"
//             });

//         fileSystem
//             .Setup(m => m.File.Exists("datei_gibt_es_nicht.txt"))
//             .Returns(false);

//         var actual = sut.ReadWords();

//         actual.Should().NotBeNull();
//         displayOutput.Verify(v => v.WriteErrorResourceLine("FILE_NOT_FOUND", "datei_gibt_es_nicht.txt"), Times.Once);
//     }

//     [Theory, AutoMoqData]
//     public void DictionaryFileLoaderTests_File_Content_WordA_WordB_Expect_WordA_WordB(
//         [Frozen] Mock<IDictionaryParameterParser> dictionaryParameterParser,
//         [Frozen] Mock<IFileSystem> fileSystem,
//         DictionaryFileLoader sut)
//     {
//         dictionaryParameterParser
//             .Setup(m => m.ParseDictionaryParameter())
//             .Returns(new DictionaryParameter
//             {
//                 IsPresent = true,
//                 FileName = It.IsAny<string>()
//             });

//         fileSystem
//             .Setup(m => m.File.Exists(It.IsAny<string>()))
//             .Returns(true);

//         fileSystem
//             .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
//             .Returns(new[] { "WordA", "WordB" });

//         var expected = new List<string> { "WordA", "WordB" };
//         var actual = sut.ReadWords();

//         actual.Should().BeEquivalentTo(expected);
//     }

//     [Theory, AutoMoqData]
//     public void DictionaryFileLoaderTests_File_Content_Null_Expect_Empty(
//         [Frozen] Mock<IDictionaryParameterParser> dictionaryParameterParser,
//         [Frozen] Mock<IFileSystem> fileSystem,
//         DictionaryFileLoader sut)
//     {
//         dictionaryParameterParser
//             .Setup(m => m.ParseDictionaryParameter())
//             .Returns(new DictionaryParameter
//             {
//                 IsPresent = true,
//                 FileName = It.IsAny<string>()
//             });

//         fileSystem
//             .Setup(m => m.File.Exists(It.IsAny<string>()))
//             .Returns(true);

//         fileSystem
//             .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
//             .Returns(() => null);

//         var actual = sut.ReadWords();

//         actual.Should().NotBeNull();
//     }

//     [Theory, AutoMoqData]
//     public void DictionaryFileLoaderTests_File_Not_Exists_Expect_Empty(
//         [Frozen] Mock<IDictionaryParameterParser> dictionaryParameterParser,
//         [Frozen] Mock<IFileSystem> fileSystem,
//         DictionaryFileLoader sut)
//     {
//         dictionaryParameterParser
//             .Setup(m => m.ParseDictionaryParameter())
//             .Returns(new DictionaryParameter
//             {
//                 IsPresent = true,
//                 FileName = It.IsAny<string>()
//             });

//         fileSystem
//             .Setup(m => m.File.Exists(It.IsAny<string>()))
//             .Returns(false);

//         fileSystem
//             .Setup(m => m.File.ReadAllLines(It.IsAny<string>()))
//             .Throws<FileNotFoundException>();
        
//         var actual = sut.ReadWords();

//         actual.Should().NotBeNull();
//     }

//     [Theory, AutoMoqData]
//     public void DictionaryFileLoaderTests_DictionaryParameter_is_not_present_do_not_call_File_ReadAllLines(
//         [Frozen] Mock<IDictionaryParameterParser> dictionaryParameterParser,
//         [Frozen] Mock<IFileSystem> fileSystem,
//         DictionaryFileLoader sut)
//     {
//         dictionaryParameterParser
//             .Setup(m => m.ParseDictionaryParameter())
//             .Returns(new DictionaryParameter { IsPresent = false });

//         sut.ReadWords();

//         fileSystem.Verify(v => v.File.ReadAllLines(null), Times.Never);
//     }
// }