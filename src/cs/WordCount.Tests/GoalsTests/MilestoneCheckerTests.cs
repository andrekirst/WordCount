using System.Linq;
using FluentAssertions;
using WordCount.Implementations.Goals;
using WordCount.Models.Goals;
using Xunit;

namespace WordCount.Tests.GoalsTests;

public class MilestoneCheckerTests
{
    private readonly MilestoneChecker _sut;

    public MilestoneCheckerTests()
    {
        _sut = new MilestoneChecker();
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_FirstGoalSet_Milestone()
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { TargetWordCount = 500 });

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().ContainSingle(m => m.Type == MilestoneType.FirstGoalSet);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Not_Award_Duplicate_Milestones()
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { TargetWordCount = 500 });
        data.Milestones.Add(new Milestone { Type = MilestoneType.FirstGoalSet });

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().NotContain(m => m.Type == MilestoneType.FirstGoalSet);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_FirstGoalCompleted_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalGoalsCompleted = 1 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().ContainSingle(m => m.Type == MilestoneType.FirstGoalCompleted);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_ThreeDayStreak_Milestone()
    {
        // Arrange
        var data = new GoalData();
        data.CurrentStreak.CurrentStreak = 3;

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.ThreeDayStreak);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_SevenDayStreak_Milestone()
    {
        // Arrange
        var data = new GoalData();
        data.CurrentStreak.CurrentStreak = 7;

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.SevenDayStreak);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_ThirtyDayStreak_Milestone()
    {
        // Arrange
        var data = new GoalData();
        data.CurrentStreak.CurrentStreak = 30;

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.ThirtyDayStreak);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_TotalWords1000_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalWordsWritten = 1000 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.TotalWords1000);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_TotalWords5000_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalWordsWritten = 5000 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.TotalWords5000);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_TotalWords10000_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalWordsWritten = 10000 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.TotalWords10000);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_TotalWords50000_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalWordsWritten = 50000 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.TotalWords50000);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_TotalWords100000_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalWordsWritten = 100000 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.TotalWords100000);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_GoalsCompleted5_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalGoalsCompleted = 5 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.GoalsCompleted5);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_GoalsCompleted10_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalGoalsCompleted = 10 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.GoalsCompleted10);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_GoalsCompleted25_Milestone()
    {
        // Arrange
        var data = new GoalData { TotalGoalsCompleted = 25 };

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().Contain(m => m.Type == MilestoneType.GoalsCompleted25);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Add_Milestones_To_Data()
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { TargetWordCount = 500 });

        // Act
        _sut.CheckForNewMilestones(data);

        // Assert
        data.Milestones.Should().Contain(m => m.Type == MilestoneType.FirstGoalSet);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Set_IsNew_True_On_New_Milestones()
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { TargetWordCount = 500 });

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().OnlyContain(m => m.IsNew);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Set_Description()
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { TargetWordCount = 500 });

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.First().Description.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void CheckForNewMilestones_Should_Award_Multiple_Milestones_At_Once()
    {
        // Arrange
        var data = new GoalData
        {
            TotalWordsWritten = 10000,
            TotalGoalsCompleted = 10
        };
        data.Goals.Add(new Goal { TargetWordCount = 500 });
        data.CurrentStreak.CurrentStreak = 7;

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Count().Should().BeGreaterOrEqualTo(4);
    }

    [Fact]
    public void GetAllMilestones_Should_Return_All_Milestones_Ordered_By_Date()
    {
        // Arrange
        var data = new GoalData();
        data.Milestones.Add(new Milestone { Type = MilestoneType.FirstGoalSet });
        data.Milestones.Add(new Milestone { Type = MilestoneType.TotalWords1000 });

        // Act
        var result = _sut.GetAllMilestones(data);

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public void GetNewMilestones_Should_Return_Only_New_Milestones()
    {
        // Arrange
        var data = new GoalData();
        data.Milestones.Add(new Milestone { Type = MilestoneType.FirstGoalSet, IsNew = true });
        data.Milestones.Add(new Milestone { Type = MilestoneType.TotalWords1000, IsNew = false });

        // Act
        var result = _sut.GetNewMilestones(data);

        // Assert
        result.Should().ContainSingle();
        result.First().Type.Should().Be(MilestoneType.FirstGoalSet);
    }

    [Fact]
    public void CheckForNewMilestones_Should_Return_Empty_When_No_New_Milestones()
    {
        // Arrange
        var data = new GoalData();

        // Act
        var result = _sut.CheckForNewMilestones(data);

        // Assert
        result.Should().BeEmpty();
    }
}
