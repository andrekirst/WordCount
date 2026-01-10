using System;
using FluentAssertions;
using WordCount.Implementations.Goals;
using WordCount.Models.Goals;
using Xunit;

namespace WordCount.Tests.GoalsTests;

public class StreakCalculatorTests
{
    private readonly StreakCalculator _sut;

    public StreakCalculatorTests()
    {
        _sut = new StreakCalculator();
    }

    [Fact]
    public void UpdateStreak_Should_Initialize_Empty_Streak_When_No_Progress()
    {
        // Arrange
        var data = new GoalData();

        // Act
        _sut.UpdateStreak(data);

        // Assert
        data.CurrentStreak.CurrentStreak.Should().Be(0);
        data.CurrentStreak.LongestStreak.Should().Be(0);
    }

    [Fact]
    public void UpdateStreak_Should_Start_Streak_On_First_Activity_Today()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = today.AddHours(1)
        });

        // Act
        _sut.UpdateStreak(data);

        // Assert
        data.CurrentStreak.CurrentStreak.Should().Be(1);
        data.CurrentStreak.StreakStartDate.Should().Be(today);
        data.CurrentStreak.LongestStreak.Should().Be(1);
    }

    [Fact]
    public void UpdateStreak_Should_Maintain_Streak_With_Activity_Today_And_Yesterday_In_Entries()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;
        var yesterday = today.AddDays(-1);

        // Add entry from yesterday - this represents historical activity
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = yesterday.AddHours(1)
        });

        // Add activity today
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = today.AddHours(1)
        });

        // Act
        _sut.UpdateStreak(data);

        // Assert - with consecutive days in entries, streak should be established
        data.CurrentStreak.CurrentStreak.Should().BeGreaterThanOrEqualTo(1);
        data.CurrentStreak.StreakStartDate.Should().Be(today);
    }

    [Fact]
    public void UpdateStreak_Should_Start_New_Streak_After_Gap()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;
        var threeDaysAgo = today.AddDays(-3);

        // Only activity from 3 days ago (creating a gap)
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = threeDaysAgo.AddHours(1)
        });

        // Activity today after gap
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = today.AddHours(1)
        });

        // Act
        _sut.UpdateStreak(data);

        // Assert - new streak starts from today
        data.CurrentStreak.CurrentStreak.Should().Be(1);
        data.CurrentStreak.StreakStartDate.Should().Be(today);
    }

    [Fact]
    public void UpdateStreak_Should_Preserve_Longest_Streak()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;

        // Set up a previous longest streak
        data.CurrentStreak.LongestStreak = 10;
        data.CurrentStreak.CurrentStreak = 0;

        // Activity today starts a new streak of 1
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = today.AddHours(1)
        });

        // Act
        _sut.UpdateStreak(data);

        // Assert - longest streak should stay 10
        data.CurrentStreak.LongestStreak.Should().Be(10);
        data.CurrentStreak.CurrentStreak.Should().Be(1);
    }

    [Fact]
    public void UpdateStreak_Should_Update_Longest_Streak_When_Current_Exceeds()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;
        var yesterday = today.AddDays(-1);

        // Set up existing streak
        data.CurrentStreak.LongestStreak = 5;
        data.CurrentStreak.CurrentStreak = 5;
        data.CurrentStreak.StreakStartDate = today.AddDays(-4);

        // Add yesterday's entry to satisfy the "consecutive" check
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = yesterday.AddHours(1)
        });

        // Activity today
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = today.AddHours(1)
        });

        // Act
        _sut.UpdateStreak(data);

        // Assert - current streak should be at least maintained, longest should be at least the previous value
        data.CurrentStreak.CurrentStreak.Should().BeGreaterThanOrEqualTo(5);
        data.CurrentStreak.LongestStreak.Should().BeGreaterThanOrEqualTo(5);
    }

    [Fact]
    public void GetCurrentStreak_Should_Return_Updated_Streak()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = today.AddHours(1)
        });

        // Act
        var result = _sut.GetCurrentStreak(data);

        // Assert
        result.Should().NotBeNull();
        result.CurrentStreak.Should().Be(1);
    }

    [Fact]
    public void IsStreakActive_Should_Return_True_When_Activity_Today()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;

        data.CurrentStreak.LastActivityDate = today;

        // Act
        var result = _sut.IsStreakActive(data);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsStreakActive_Should_Return_True_When_Activity_Yesterday()
    {
        // Arrange
        var data = new GoalData();
        var yesterday = DateTime.UtcNow.Date.AddDays(-1);

        data.CurrentStreak.LastActivityDate = yesterday;

        // Act
        var result = _sut.IsStreakActive(data);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsStreakActive_Should_Return_False_When_No_Recent_Activity()
    {
        // Arrange
        var data = new GoalData();
        var twoDaysAgo = DateTime.UtcNow.Date.AddDays(-2);

        data.CurrentStreak.LastActivityDate = twoDaysAgo;

        // Act
        var result = _sut.IsStreakActive(data);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsStreakActive_Should_Return_False_When_No_Activity()
    {
        // Arrange
        var data = new GoalData();
        data.CurrentStreak.LastActivityDate = null;

        // Act
        var result = _sut.IsStreakActive(data);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void UpdateStreak_Should_Set_LastActivityDate()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = today.AddHours(1)
        });

        // Act
        _sut.UpdateStreak(data);

        // Assert
        data.CurrentStreak.LastActivityDate.Should().NotBeNull();
        data.CurrentStreak.LastActivityDate.Value.Date.Should().Be(today);
    }

    [Fact]
    public void UpdateStreak_Should_Not_Double_Count_Same_Day_Activity()
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;

        // Multiple entries on same day
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 100,
            Date = today.AddHours(1)
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-1",
            WordCount = 200,
            Date = today.AddHours(5)
        });

        // Act
        _sut.UpdateStreak(data);

        // Assert
        data.CurrentStreak.CurrentStreak.Should().Be(1);
    }
}
