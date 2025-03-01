// using Moq;
// using System;
// using AutoFixture.Xunit2;
// using FluentAssertions;
// using WordCount.Abstractions.SystemAbstractions;
// using WordCount.Implementations.ArgumentsHandling;
// using Xunit;

// namespace WordCount.Tests.ArgumentsHandlingTests;

// public class StopwordListParameterParserTests
// {
//     [Theory, AutoMoqData]
//     public void StopwordListParameterParserTests_Args_have_no_Dictionary_Parameter_Expect_IsPresent_False(
//         [Frozen] Mock<IEnvironment> environment,
//         StopwordListParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(new[] { "bla.txt" });

//         var actual = sut.ParseStopwordListParameter();

//         actual.Should().NotBeNull();
//         actual.IsPresent.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public void StopwordListParameterParserTests_Args_is_null_Expect_IsPresent_False(
//         [Frozen] Mock<IEnvironment> environment,
//         StopwordListParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(() => null);

//         var actual = sut.ParseStopwordListParameter();

//         actual.Should().NotBeNull();
//         actual.IsPresent.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public void StopwordListParameterParserTests_Args_is_empty_Expect_IsPresent_False(
//         [Frozen] Mock<IEnvironment> environment,
//         StopwordListParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(Array.Empty<string>());

//         var actual = sut.ParseStopwordListParameter();

//         actual.Should().NotBeNull();
//         actual.IsPresent.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public void StopwordListParameterParserTests_Args_has_StopwordListParameter_with_no_equal_sign_Expect_IsPresent_False(
//         [Frozen] Mock<IEnvironment> environment,
//         StopwordListParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(new[] { "-stopwordlist" });

//         var actual = sut.ParseStopwordListParameter();

//         actual.Should().NotBeNull();
//         actual.IsPresent.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public void StopwordListParameterParserTests_Args_has_StopwordListParameter_Without_File_Expect_IsPresent_False(
//         [Frozen] Mock<IEnvironment> environment,
//         StopwordListParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(new[] { "-stopwordlist=" });

//         var actual = sut.ParseStopwordListParameter();

//         actual.Should().NotBeNull();
//         actual.IsPresent.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public void StopwordListParameterParserTests_Args_has_StopwordListParameter_With_File_bla_txt_Expect_FileName_bla_txt(
//         [Frozen] Mock<IEnvironment> environment,
//         StopwordListParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(new[] { "-stopwordlist=bla.txt" });

//         var actual = sut.ParseStopwordListParameter();

//         actual.Should().NotBeNull();
//         actual.FileName.Should().Be("bla.txt");
//     }
// }