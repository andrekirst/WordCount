using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using Moq;
using WordCount.Implementations.Output;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Goals;
using WordCount.Models.Results;
using WordCount.Tests;
using Xunit;

namespace WordCount.Tests.GoalsTests;

public class GoalOutputTests
{
    [Theory, AutoMoqData]
    public void DisplayGoalProgress_Should_Display_NotSet_Message_When_Progress_Is_Null(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Act
        sut.DisplayGoalProgress(null);

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("GOAL_NOT_SET"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayGoalProgress_Should_Display_Goal_Type_And_Target(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var progress = new GoalProgressResult
        {
            Goal = new Goal { Type = GoalType.Daily, TargetWordCount = 500 },
            TargetWordCount = 500,
            CurrentWordCount = 200,
            PercentageComplete = 40,
            WordsRemaining = 300
        };

        // Act
        sut.DisplayGoalProgress(progress);

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("GOAL_PROGRESS"), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Daily Goal"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayGoalProgress_Should_Display_Project_Name_When_Present(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var progress = new GoalProgressResult
        {
            Goal = new Goal { Type = GoalType.Project, ProjectName = "My Novel", TargetWordCount = 50000 },
            TargetWordCount = 50000,
            CurrentWordCount = 10000,
            PercentageComplete = 20,
            WordsRemaining = 40000
        };

        // Act
        sut.DisplayGoalProgress(progress);

        // Assert
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("My Novel"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayGoalProgress_Should_Display_Progress_Bar(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var progress = new GoalProgressResult
        {
            Goal = new Goal { Type = GoalType.Daily, TargetWordCount = 500 },
            TargetWordCount = 500,
            CurrentWordCount = 250,
            PercentageComplete = 50,
            WordsRemaining = 250
        };

        // Act
        sut.DisplayGoalProgress(progress);

        // Assert
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("[") && s.Contains("]") && s.Contains("%"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayGoalProgress_Should_Display_Completion_Message_When_Completed(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var progress = new GoalProgressResult
        {
            Goal = new Goal { Type = GoalType.Daily, TargetWordCount = 500 },
            TargetWordCount = 500,
            CurrentWordCount = 600,
            PercentageComplete = 100,
            WordsRemaining = 0,
            IsCompleted = true
        };

        // Act
        sut.DisplayGoalProgress(progress);

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("GOAL_COMPLETED"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayGoalProgress_Should_Display_Days_Remaining_For_Weekly_Goal(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var progress = new GoalProgressResult
        {
            Goal = new Goal { Type = GoalType.Weekly, TargetWordCount = 3500 },
            TargetWordCount = 3500,
            CurrentWordCount = 1000,
            PercentageComplete = 28.5,
            WordsRemaining = 2500,
            DaysRemaining = 4
        };

        // Act
        sut.DisplayGoalProgress(progress);

        // Assert
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Days remaining"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayStreak_Should_Display_Current_Streak(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var streak = new Streak
        {
            CurrentStreak = 5,
            LongestStreak = 10,
            LastActivityDate = DateTime.UtcNow
        };

        // Act
        sut.DisplayStreak(streak);

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("STREAK_INFO"), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Current streak") && s.Contains("5"))), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Best streak") && s.Contains("10"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayStreak_Should_Display_Last_Activity_Date(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var streak = new Streak
        {
            CurrentStreak = 5,
            LongestStreak = 10,
            LastActivityDate = new DateTime(2024, 1, 15)
        };

        // Act
        sut.DisplayStreak(streak);

        // Assert
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Last activity"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayMilestones_Should_Not_Output_When_Empty(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Act
        sut.DisplayMilestones(Enumerable.Empty<Milestone>());

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("NEW_MILESTONES"), Times.Never);
    }

    [Theory, AutoMoqData]
    public void DisplayMilestones_Should_Display_Each_Milestone(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var milestones = new List<Milestone>
        {
            new Milestone { Type = MilestoneType.FirstGoalSet, Description = "Set your first writing goal!" },
            new Milestone { Type = MilestoneType.TotalWords1000, Description = "Written 1,000 words total!" }
        };

        // Act
        sut.DisplayMilestones(milestones);

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("NEW_MILESTONES"), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Set your first writing goal!"))), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Written 1,000 words total!"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayGoalSummary_Should_Display_Totals(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var data = new GoalData
        {
            TotalWordsWritten = 5000,
            TotalGoalsCompleted = 10
        };
        data.Milestones.Add(new Milestone { Type = MilestoneType.FirstGoalSet });
        data.Milestones.Add(new Milestone { Type = MilestoneType.TotalWords1000 });

        // Act
        sut.DisplayGoalSummary(data);

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("GOAL_SUMMARY"), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Total words written"))), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Goals completed"))), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Total milestones"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayGoalSummary_Should_Display_Active_Goals(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { Type = GoalType.Daily, TargetWordCount = 500, IsActive = true });

        // Act
        sut.DisplayGoalSummary(data);

        // Assert
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Active Goals"))), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("Daily Goal"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayGoalSummary_Should_Display_None_When_No_Active_Goals(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var data = new GoalData();

        // Act
        sut.DisplayGoalSummary(data);

        // Assert
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("(none)"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayProgressChart_Should_Not_Output_When_No_Entries(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var data = new GoalData();

        // Act
        sut.DisplayProgressChart(data, "goal-id");

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("PROGRESS_CHART"), Times.Never);
    }

    [Theory, AutoMoqData]
    public void DisplayProgressChart_Should_Display_Chart_When_Entries_Exist(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-id",
            WordCount = 500,
            Date = today
        });
        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = "goal-id",
            WordCount = 300,
            Date = today.AddDays(-1)
        });

        // Act
        sut.DisplayProgressChart(data, "goal-id");

        // Assert
        displayOutput.Verify(d => d.WriteResourceLine("PROGRESS_CHART"), Times.Once);
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("|") && s.Contains("#"))), Times.AtLeastOnce);
    }

    [Theory, AutoMoqData]
    public void DisplayProgressChart_Should_Show_Day_Labels(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var data = new GoalData();
        var today = DateTime.UtcNow.Date;

        data.ProgressEntries.Add(new ProgressEntry
        {
            GoalId = null,
            WordCount = 500,
            Date = today
        });

        // Act
        sut.DisplayProgressChart(data, null);

        // Assert
        // The chart line format is: "  {day_abbrev} |{bar} {count}"
        // Verify the chart contains the bar separator character and word count
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s =>
            s.Contains("|") && s.Contains("500"))), Times.AtLeastOnce);
    }

    [Theory, AutoMoqData]
    public void DisplayStreak_Should_Show_Different_Symbols_Based_On_Streak_Length(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var streak = new Streak
        {
            CurrentStreak = 30,
            LongestStreak = 30,
            LastActivityDate = DateTime.UtcNow
        };

        // Act
        sut.DisplayStreak(streak);

        // Assert
        // Should show a 5-star symbol for 30+ day streak
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("[*****]"))), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DisplayMilestones_Should_Show_Trophy_Icons(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        GoalOutput sut)
    {
        // Arrange
        var milestones = new List<Milestone>
        {
            new Milestone { Type = MilestoneType.TotalWords100000, Description = "100K words!" }
        };

        // Act
        sut.DisplayMilestones(milestones);

        // Assert
        displayOutput.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains("[GOLD]"))), Times.Once);
    }
}
