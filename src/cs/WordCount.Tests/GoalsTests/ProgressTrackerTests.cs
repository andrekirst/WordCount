using System;
using System.Linq;
using FluentAssertions;
using WordCount.Implementations.Goals;
using WordCount.Models.Goals;
using Xunit;

namespace WordCount.Tests.GoalsTests;

public class ProgressTrackerTests
{
    private readonly ProgressTracker _sut;

    public ProgressTrackerTests()
    {
        _sut = new ProgressTracker();
    }

    [Fact]
    public void RecordProgress_Should_Add_Entry_To_Data()
    {
        // Arrange
        var data = new GoalData();
        var goalId = "test-goal-id";
        var wordCount = 500;

        // Act
        _sut.RecordProgress(data, wordCount, goalId);

        // Assert
        data.ProgressEntries.Should().ContainSingle();
        data.ProgressEntries[0].GoalId.Should().Be(goalId);
        data.ProgressEntries[0].WordCount.Should().Be(wordCount);
    }

    [Fact]
    public void RecordProgress_Should_Update_TotalWordsWritten()
    {
        // Arrange
        var data = new GoalData { TotalWordsWritten = 100 };
        var goalId = "test-goal-id";

        // Act
        _sut.RecordProgress(data, 500, goalId);

        // Assert
        data.TotalWordsWritten.Should().Be(600);
    }

    [Fact]
    public void RecordProgress_Should_Store_SourceFile()
    {
        // Arrange
        var data = new GoalData();
        var goalId = "test-goal-id";

        // Act
        _sut.RecordProgress(data, 500, goalId, "chapter1.txt");

        // Assert
        data.ProgressEntries[0].SourceFile.Should().Be("chapter1.txt");
    }

    [Fact]
    public void GetProgressForPeriod_Should_Return_Sum_Of_Words_In_Period()
    {
        // Arrange
        var data = new GoalData();
        var goalId = "test-goal-id";
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 100,
            Date = today.AddHours(1)
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 200,
            Date = today.AddHours(5)
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 300,
            Date = today.AddDays(-1) // Outside period
        });

        // Act
        var result = _sut.GetProgressForPeriod(data, goalId, today, today.AddDays(1));

        // Assert
        result.Should().Be(300); // 100 + 200
    }

    [Fact]
    public void GetProgressForPeriod_Should_Filter_By_GoalId()
    {
        // Arrange
        var data = new GoalData();
        var goalId = "test-goal-id";
        var otherGoalId = "other-goal-id";
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 100,
            Date = today.AddHours(1)
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = otherGoalId,
            WordCount = 500,
            Date = today.AddHours(2)
        });

        // Act
        var result = _sut.GetProgressForPeriod(data, goalId, today, today.AddDays(1));

        // Assert
        result.Should().Be(100);
    }

    [Fact]
    public void GetTodayProgress_Should_Return_Words_From_Today()
    {
        // Arrange
        var data = new GoalData();
        var goalId = "test-goal-id";
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 100,
            Date = today.AddHours(1)
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 200,
            Date = today.AddDays(-1)
        });

        // Act
        var result = _sut.GetTodayProgress(data, goalId);

        // Assert
        result.Should().Be(100);
    }

    [Fact]
    public void GetWeekProgress_Should_Return_Words_From_Current_Week()
    {
        // Arrange
        var data = new GoalData();
        var goalId = "test-goal-id";
        var today = DateTime.UtcNow.Date;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 100,
            Date = startOfWeek.AddHours(1)
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 200,
            Date = startOfWeek.AddDays(1)
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 300,
            Date = startOfWeek.AddDays(-1) // Previous week
        });

        // Act
        var result = _sut.GetWeekProgress(data, goalId);

        // Assert
        result.Should().Be(300); // 100 + 200
    }

    [Fact]
    public void GetAverageWordsPerDay_Should_Return_Zero_When_No_Entries()
    {
        // Arrange
        var data = new GoalData();
        var goalId = "test-goal-id";

        // Act
        var result = _sut.GetAverageWordsPerDay(data, goalId);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void GetAverageWordsPerDay_Should_Calculate_Correctly()
    {
        // Arrange
        var data = new GoalData();
        var goalId = "test-goal-id";
        var today = DateTime.UtcNow.Date;

        // 1000 words over 5 days = 200 words per day
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 500,
            Date = today
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goalId,
            WordCount = 500,
            Date = today.AddDays(-4)
        });

        // Act
        var result = _sut.GetAverageWordsPerDay(data, goalId);

        // Assert
        result.Should().Be(200); // 1000 words / 5 days
    }

    [Fact]
    public void CalculateGoalProgress_Should_Return_Correct_Progress_For_Daily_Goal()
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal
        {
            Id = "goal-id",
            Type = GoalType.Daily,
            TargetWordCount = 500
        };
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goal.Id,
            WordCount = 200,
            Date = today.AddHours(1)
        });

        // Act
        var result = _sut.CalculateGoalProgress(data, goal);

        // Assert
        result.Goal.Should().Be(goal);
        result.CurrentWordCount.Should().Be(200);
        result.TargetWordCount.Should().Be(500);
        result.WordsRemaining.Should().Be(300);
        result.PercentageComplete.Should().Be(40);
        result.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public void CalculateGoalProgress_Should_Mark_Goal_Completed_When_Target_Reached()
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal
        {
            Id = "goal-id",
            Type = GoalType.Daily,
            TargetWordCount = 500
        };
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goal.Id,
            WordCount = 600,
            Date = today.AddHours(1)
        });

        // Act
        var result = _sut.CalculateGoalProgress(data, goal);

        // Assert
        result.IsCompleted.Should().BeTrue();
        result.PercentageComplete.Should().Be(100);
        result.WordsRemaining.Should().Be(0);
    }

    [Fact]
    public void CalculateGoalProgress_Should_Calculate_Days_Remaining_For_Weekly_Goal()
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal
        {
            Id = "goal-id",
            Type = GoalType.Weekly,
            TargetWordCount = 3500
        };

        // Act
        var result = _sut.CalculateGoalProgress(data, goal);

        // Assert
        result.DaysRemaining.Should().BeGreaterOrEqualTo(0);
        result.DaysRemaining.Should().BeLessOrEqualTo(7);
    }

    [Fact]
    public void CalculateGoalProgress_Should_Calculate_Days_Remaining_For_Project_Goal()
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal
        {
            Id = "goal-id",
            Type = GoalType.Project,
            TargetWordCount = 50000,
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var result = _sut.CalculateGoalProgress(data, goal);

        // Assert
        result.DaysRemaining.Should().BeCloseTo(30, 1);
    }

    [Fact]
    public void CalculateGoalProgress_Should_Calculate_RequiredWordsPerDay()
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal
        {
            Id = "goal-id",
            Type = GoalType.Project,
            TargetWordCount = 3000,
            EndDate = DateTime.UtcNow.AddDays(10)
        };

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goal.Id,
            WordCount = 1000,
            Date = DateTime.UtcNow
        });

        // Act
        var result = _sut.CalculateGoalProgress(data, goal);

        // Assert
        // 2000 remaining / 10 days = 200 words per day
        result.RequiredWordsPerDayToComplete.Should().BeApproximately(200, 25);
    }

    [Fact]
    public void CalculateGoalProgress_Should_Cap_Percentage_At_100()
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal
        {
            Id = "goal-id",
            Type = GoalType.Daily,
            TargetWordCount = 500
        };
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = goal.Id,
            WordCount = 1000, // Exceeded goal
            Date = today.AddHours(1)
        });

        // Act
        var result = _sut.CalculateGoalProgress(data, goal);

        // Assert
        result.PercentageComplete.Should().Be(100);
    }
}
