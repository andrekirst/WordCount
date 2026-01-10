using System;
using FluentAssertions;
using WordCount.Models.Goals;
using Xunit;

namespace WordCount.Tests.GoalsTests;

public class GoalModelTests
{
    [Fact]
    public void Goal_Constructor_Should_Generate_Unique_Id()
    {
        // Arrange & Act
        var goal = new Goal();

        // Assert
        goal.Id.Should().NotBeNullOrEmpty();
        Guid.TryParse(goal.Id, out _).Should().BeTrue();
    }

    [Fact]
    public void Goal_Constructor_Should_Set_CreatedAt_To_UtcNow()
    {
        // Arrange
        var before = DateTime.UtcNow;

        // Act
        var goal = new Goal();

        // Assert
        var after = DateTime.UtcNow;
        goal.CreatedAt.Should().BeOnOrAfter(before);
        goal.CreatedAt.Should().BeOnOrBefore(after);
    }

    [Fact]
    public void Goal_Constructor_Should_Set_IsActive_To_True()
    {
        // Act
        var goal = new Goal();

        // Assert
        goal.IsActive.Should().BeTrue();
    }

    [Theory]
    [InlineData(GoalType.Daily)]
    [InlineData(GoalType.Weekly)]
    [InlineData(GoalType.Project)]
    public void Goal_Should_Accept_All_GoalTypes(GoalType type)
    {
        // Act
        var goal = new Goal { Type = type };

        // Assert
        goal.Type.Should().Be(type);
    }

    [Fact]
    public void Goal_Should_Store_TargetWordCount()
    {
        // Act
        var goal = new Goal { TargetWordCount = 1000 };

        // Assert
        goal.TargetWordCount.Should().Be(1000);
    }

    [Fact]
    public void Goal_Should_Store_ProjectName()
    {
        // Act
        var goal = new Goal { ProjectName = "My Novel" };

        // Assert
        goal.ProjectName.Should().Be("My Novel");
    }

    [Fact]
    public void Goal_Should_Store_DateRange()
    {
        // Arrange
        var startDate = DateTime.UtcNow.Date;
        var endDate = startDate.AddDays(7);

        // Act
        var goal = new Goal
        {
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        goal.StartDate.Should().Be(startDate);
        goal.EndDate.Should().Be(endDate);
    }
}

public class ProgressEntryTests
{
    [Fact]
    public void ProgressEntry_Constructor_Should_Generate_Unique_Id()
    {
        // Act
        var entry = new ProgressEntry();

        // Assert
        entry.Id.Should().NotBeNullOrEmpty();
        Guid.TryParse(entry.Id, out _).Should().BeTrue();
    }

    [Fact]
    public void ProgressEntry_Constructor_Should_Set_Date_To_UtcNow()
    {
        // Arrange
        var before = DateTime.UtcNow;

        // Act
        var entry = new ProgressEntry();

        // Assert
        var after = DateTime.UtcNow;
        entry.Date.Should().BeOnOrAfter(before);
        entry.Date.Should().BeOnOrBefore(after);
    }

    [Fact]
    public void ProgressEntry_Should_Store_WordCount()
    {
        // Act
        var entry = new ProgressEntry { WordCount = 500 };

        // Assert
        entry.WordCount.Should().Be(500);
    }

    [Fact]
    public void ProgressEntry_Should_Store_GoalId()
    {
        // Act
        var goalId = Guid.NewGuid().ToString();
        var entry = new ProgressEntry { GoalId = goalId };

        // Assert
        entry.GoalId.Should().Be(goalId);
    }

    [Fact]
    public void ProgressEntry_Should_Store_SourceFile()
    {
        // Act
        var entry = new ProgressEntry { SourceFile = "chapter1.txt" };

        // Assert
        entry.SourceFile.Should().Be("chapter1.txt");
    }
}

public class StreakTests
{
    [Fact]
    public void Streak_Should_Initialize_With_Default_Values()
    {
        // Act
        var streak = new Streak();

        // Assert
        streak.CurrentStreak.Should().Be(0);
        streak.LongestStreak.Should().Be(0);
        streak.LastActivityDate.Should().BeNull();
        streak.StreakStartDate.Should().BeNull();
    }

    [Fact]
    public void Streak_Should_Store_CurrentStreak()
    {
        // Act
        var streak = new Streak { CurrentStreak = 5 };

        // Assert
        streak.CurrentStreak.Should().Be(5);
    }

    [Fact]
    public void Streak_Should_Store_LongestStreak()
    {
        // Act
        var streak = new Streak { LongestStreak = 10 };

        // Assert
        streak.LongestStreak.Should().Be(10);
    }
}

public class MilestoneTests
{
    [Fact]
    public void Milestone_Constructor_Should_Set_AchievedAt_To_UtcNow()
    {
        // Arrange
        var before = DateTime.UtcNow;

        // Act
        var milestone = new Milestone();

        // Assert
        var after = DateTime.UtcNow;
        milestone.AchievedAt.Should().BeOnOrAfter(before);
        milestone.AchievedAt.Should().BeOnOrBefore(after);
    }

    [Fact]
    public void Milestone_Constructor_Should_Set_IsNew_To_True()
    {
        // Act
        var milestone = new Milestone();

        // Assert
        milestone.IsNew.Should().BeTrue();
    }

    [Theory]
    [InlineData(MilestoneType.FirstGoalSet)]
    [InlineData(MilestoneType.FirstGoalCompleted)]
    [InlineData(MilestoneType.ThreeDayStreak)]
    [InlineData(MilestoneType.SevenDayStreak)]
    [InlineData(MilestoneType.ThirtyDayStreak)]
    [InlineData(MilestoneType.TotalWords1000)]
    [InlineData(MilestoneType.TotalWords50000)]
    [InlineData(MilestoneType.GoalsCompleted5)]
    public void Milestone_Should_Accept_All_MilestoneTypes(MilestoneType type)
    {
        // Act
        var milestone = new Milestone { Type = type };

        // Assert
        milestone.Type.Should().Be(type);
    }

    [Fact]
    public void Milestone_Should_Store_Description()
    {
        // Act
        var milestone = new Milestone { Description = "Test milestone" };

        // Assert
        milestone.Description.Should().Be("Test milestone");
    }
}

public class GoalDataTests
{
    [Fact]
    public void GoalData_Constructor_Should_Initialize_Empty_Lists()
    {
        // Act
        var data = new GoalData();

        // Assert
        data.Goals.Should().NotBeNull();
        data.Goals.Should().BeEmpty();
        data.ProgressEntries.Should().NotBeNull();
        data.ProgressEntries.Should().BeEmpty();
        data.Milestones.Should().NotBeNull();
        data.Milestones.Should().BeEmpty();
    }

    [Fact]
    public void GoalData_Constructor_Should_Initialize_CurrentStreak()
    {
        // Act
        var data = new GoalData();

        // Assert
        data.CurrentStreak.Should().NotBeNull();
        data.CurrentStreak.CurrentStreak.Should().Be(0);
    }

    [Fact]
    public void GoalData_Constructor_Should_Initialize_Counters_To_Zero()
    {
        // Act
        var data = new GoalData();

        // Assert
        data.TotalWordsWritten.Should().Be(0);
        data.TotalGoalsCompleted.Should().Be(0);
    }

    [Fact]
    public void GoalData_Should_Store_Goals()
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal { TargetWordCount = 1000 };

        // Act
        data.Goals.Add(goal);

        // Assert
        data.Goals.Should().ContainSingle();
        data.Goals[0].TargetWordCount.Should().Be(1000);
    }

    [Fact]
    public void GoalData_Should_Store_ProgressEntries()
    {
        // Arrange
        var data = new GoalData();
        var entry = new ProgressEntry { WordCount = 500 };

        // Act
        data.ProgressEntries.Add(entry);

        // Assert
        data.ProgressEntries.Should().ContainSingle();
        data.ProgressEntries[0].WordCount.Should().Be(500);
    }

    [Fact]
    public void GoalData_Should_Store_Milestones()
    {
        // Arrange
        var data = new GoalData();
        var milestone = new Milestone { Type = MilestoneType.FirstGoalSet };

        // Act
        data.Milestones.Add(milestone);

        // Assert
        data.Milestones.Should().ContainSingle();
        data.Milestones[0].Type.Should().Be(MilestoneType.FirstGoalSet);
    }
}
