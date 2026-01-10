using Moq;
using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests;

public class FolderParameterParserTests
{
    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_is_empty_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(Array.Empty<string>());

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_is_null_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(() => null);

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_has_f_flag_Expect_IsPresent_True(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-f", "/path/to/folder" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_has_folder_flag_Expect_IsPresent_True(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "--folder", "/path/to/folder" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_has_f_flag_Expect_FolderPath_Correct(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-f", "/my/documents/folder" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.FolderPath.Should().Be("/my/documents/folder");
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_without_pattern_flag_Expect_Default_Pattern(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-f", "/path/to/folder" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.FilePattern.Should().Be("*.txt");
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_with_p_pattern_flag_Expect_Custom_Pattern(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-f", "/path/to/folder", "-p", "*.md" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.FilePattern.Should().Be("*.md");
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_with_pattern_flag_Expect_Custom_Pattern(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-f", "/path/to/folder", "--pattern", "*.doc" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.FilePattern.Should().Be("*.doc");
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_has_f_flag_without_value_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-f" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_has_uppercase_F_flag_Expect_IsPresent_True(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-F", "/path/to/folder" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_with_other_flags_before_folder_flag(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-lang=de", "-f", "/path/to/folder", "-p", "*.txt" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
        actual.FolderPath.Should().Be("/path/to/folder");
        actual.FilePattern.Should().Be("*.txt");
    }

    [Theory, AutoMoqData]
    public void FolderParameterParserTests_Args_has_empty_folder_path_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        FolderParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-f", "" });

        var actual = sut.ParseFolderParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }
}
