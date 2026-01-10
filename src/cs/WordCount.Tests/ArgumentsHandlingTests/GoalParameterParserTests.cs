using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models.Goals;
using WordCount.Tests;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests;

public class GoalParameterParserTests
{
    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Return_NotPresent_When_Args_Empty(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(Array.Empty<string>());

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.Should().NotBeNull();
        result.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Return_NotPresent_When_Args_Null(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns((string[])null);

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.Should().NotBeNull();
        result.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_Daily_Goal(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--goal-daily=500" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeTrue();
        result.GoalType.Should().Be(GoalType.Daily);
        result.TargetWordCount.Should().Be(500);
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_Weekly_Goal(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--goal-weekly=3500" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeTrue();
        result.GoalType.Should().Be(GoalType.Weekly);
        result.TargetWordCount.Should().Be(3500);
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_Project_Goal(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--goal-project=50000" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeTrue();
        result.GoalType.Should().Be(GoalType.Project);
        result.TargetWordCount.Should().Be(50000);
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_Project_Name(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "--goal-project=50000", "--project-name=My Novel" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.ProjectName.Should().Be("My Novel");
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_ShowProgress_Flag(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--goal-progress" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeTrue();
        result.ShowProgress.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_ShowStats_Flag(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--goal-stats" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeTrue();
        result.ShowStats.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Be_Case_Insensitive(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--GOAL-DAILY=500" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeTrue();
        result.GoalType.Should().Be(GoalType.Daily);
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Handle_Invalid_Word_Count(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--goal-daily=invalid" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeTrue();
        result.GoalType.Should().Be(GoalType.Daily);
        result.TargetWordCount.Should().BeNull();
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_Multiple_Flags_Together(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "--goal-daily=500", "--goal-progress", "--goal-stats" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeTrue();
        result.GoalType.Should().Be(GoalType.Daily);
        result.ShowProgress.Should().BeTrue();
        result.ShowStats.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Ignore_Unrelated_Args(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-lang=de", "sample.txt", "-index" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.IsPresent.Should().BeFalse();
        result.GoalType.Should().BeNull();
        result.ShowProgress.Should().BeFalse();
        result.ShowStats.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_Large_Word_Count(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--goal-project=100000" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.TargetWordCount.Should().Be(100000);
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Handle_Zero_Word_Count(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--goal-daily=0" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.TargetWordCount.Should().Be(0);
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Parse_Project_Name_With_Spaces(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "--project-name=My Awesome Novel" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.ProjectName.Should().Be("My Awesome Novel");
    }

    [Theory, AutoMoqData]
    public void ParseGoalParameter_Should_Handle_Mixed_Case_Flags(
        [Frozen] Mock<IEnvironment> environment,
        GoalParameterParser sut)
    {
        // Arrange
        environment.Setup(m => m.GetCommandLineArgs()).Returns(new[] { "--Goal-Progress" });

        // Act
        var result = sut.ParseGoalParameter();

        // Assert
        result.ShowProgress.Should().BeTrue();
    }
}
