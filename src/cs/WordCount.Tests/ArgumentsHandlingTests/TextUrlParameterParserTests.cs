// using Moq;
// using System;
// using AutoFixture.Xunit2;
// using FluentAssertions;
// using WordCount.Abstractions.SystemAbstractions;
// using WordCount.Implementations.ArgumentsHandling;
// using Xunit;

// namespace WordCount.Tests.ArgumentsHandlingTests;

// public class TextUrlParameterParserTests
// {
//     [Theory, AutoMoqData]
//     public void TextUrlParameterParserTests_Args_is_empty_Expect_IsPresent_False(
//         [Frozen ]Mock<IEnvironment> environment,
//         TextUrlParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(Array.Empty<string>());

//         var actual = sut.ParseTextUrlParameter();

//         actual.IsPresent.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public void TextUrlParameterParserTests_Args_is_null_Expect_IsPresent_False(
//         [Frozen] Mock<IEnvironment> environment,
//         TextUrlParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(() => null);

//         var actual = sut.ParseTextUrlParameter();

//         actual.IsPresent.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public void TextUrlParameterParserTests_Args_has_texturl_equalsign_wwwaddress_Expect_IsPresent_True(
//         [Frozen] Mock<IEnvironment> environment,
//         TextUrlParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(new[] { "-texturl=http://www.google.de" });

//         var actual = sut.ParseTextUrlParameter();

//         actual.IsPresent.Should().BeTrue();
//     }

//     [Theory, AutoMoqData]
//     public void TextUrlParameterParserTests_Args_has_texturl_equalsign_url_Expect_url(
//         [Frozen] Mock<IEnvironment> environment,
//         TextUrlParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(new[] { "-texturl=http://www.google.de" });

//         var actual = sut.ParseTextUrlParameter();

//         actual.Url.Should().Be("http://www.google.de");
//     }

//     [Theory, AutoMoqData]
//     public void TextUrlParameterParserTests_Args_has_texturl_equalsign_invalid_wwwaddress_Expect_IsPresent_False(
//         [Frozen] Mock<IEnvironment> environment,
//         TextUrlParameterParser sut)
//     {
//         environment
//             .Setup(m => m.GetCommandLineArgs())
//             .Returns(new[] { "-texturl=www.googlede" });

//         var actual = sut.ParseTextUrlParameter();

//         actual.IsPresent.Should().BeFalse();
//     }
// }