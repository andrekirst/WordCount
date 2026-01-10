using System;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Implementations.Goals;
using WordCount.Interfaces.Goals;
using WordCount.Models.Goals;
using WordCount.Models.Results;
using WordCount.Tests;
using Xunit;

namespace WordCount.Tests.GoalsTests;

public class GoalManagerTests
{
    [Theory, AutoMoqData]
    public void CreateGoal_Should_Create_Daily_Goal_With_Correct_Properties(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker,
        GoalManager sut)
    {
        // Arrange
        repository.Setup(r => r.Load()).Returns(new GoalData());

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var result = manager.CreateGoal(GoalType.Daily, 500);

        // Assert
        result.Should().NotBeNull();
        result.Type.Should().Be(GoalType.Daily);
        result.TargetWordCount.Should().Be(500);
        result.IsActive.Should().BeTrue();
        result.StartDate.Should().Be(DateTime.UtcNow.Date);
        result.EndDate.Should().Be(DateTime.UtcNow.Date.AddDays(1));
    }

    [Theory, AutoMoqData]
    public void CreateGoal_Should_Create_Weekly_Goal_With_Correct_Dates(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        repository.Setup(r => r.Load()).Returns(new GoalData());

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        var today = DateTime.UtcNow.Date;
        var expectedStartOfWeek = today.AddDays(-(int)today.DayOfWeek);
        var expectedEndOfWeek = expectedStartOfWeek.AddDays(7);

        // Act
        var result = manager.CreateGoal(GoalType.Weekly, 3500);

        // Assert
        result.Type.Should().Be(GoalType.Weekly);
        result.StartDate.Should().Be(expectedStartOfWeek);
        result.EndDate.Should().Be(expectedEndOfWeek);
    }

    [Theory, AutoMoqData]
    public void CreateGoal_Should_Create_Project_Goal_With_ProjectName(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        repository.Setup(r => r.Load()).Returns(new GoalData());

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var result = manager.CreateGoal(GoalType.Project, 50000, "My Novel");

        // Assert
        result.Type.Should().Be(GoalType.Project);
        result.TargetWordCount.Should().Be(50000);
        result.ProjectName.Should().Be("My Novel");
        result.StartDate.Should().Be(DateTime.UtcNow.Date);
    }

    [Theory, AutoMoqData]
    public void CreateGoal_Should_Deactivate_Previous_Goals_Of_Same_Type(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var existingData = new GoalData();
        var existingGoal = new Goal { Type = GoalType.Daily, TargetWordCount = 300, IsActive = true };
        existingData.Goals.Add(existingGoal);

        repository.Setup(r => r.Load()).Returns(existingData);

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var newGoal = manager.CreateGoal(GoalType.Daily, 500);

        // Assert
        existingGoal.IsActive.Should().BeFalse();
        newGoal.IsActive.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void CreateGoal_Should_Save_Data(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        repository.Setup(r => r.Load()).Returns(new GoalData());

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        manager.CreateGoal(GoalType.Daily, 500);

        // Assert
        repository.Verify(r => r.Save(It.IsAny<GoalData>()), Times.Once);
    }

    [Theory, AutoMoqData]
    public void CreateGoal_Should_Check_For_Milestones(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        repository.Setup(r => r.Load()).Returns(new GoalData());

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        manager.CreateGoal(GoalType.Daily, 500);

        // Assert
        milestoneChecker.Verify(m => m.CheckForNewMilestones(It.IsAny<GoalData>()), Times.Once);
    }

    [Theory, AutoMoqData]
    public void GetActiveGoal_Should_Return_Active_Goal(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var data = new GoalData();
        var activeGoal = new Goal { Id = "active-goal", IsActive = true };
        var inactiveGoal = new Goal { Id = "inactive-goal", IsActive = false };
        data.Goals.Add(inactiveGoal);
        data.Goals.Add(activeGoal);

        repository.Setup(r => r.Load()).Returns(data);

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var result = manager.GetActiveGoal();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("active-goal");
    }

    [Theory, AutoMoqData]
    public void GetActiveGoal_Should_Return_Null_When_No_Active_Goals(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { IsActive = false });

        repository.Setup(r => r.Load()).Returns(data);

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var result = manager.GetActiveGoal();

        // Assert
        result.Should().BeNull();
    }

    [Theory, AutoMoqData]
    public void GetAllGoals_Should_Return_Goals_Ordered_By_CreatedAt(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { Id = "goal-1", CreatedAt = DateTime.UtcNow.AddDays(-2) });
        data.Goals.Add(new Goal { Id = "goal-2", CreatedAt = DateTime.UtcNow.AddDays(-1) });
        data.Goals.Add(new Goal { Id = "goal-3", CreatedAt = DateTime.UtcNow });

        repository.Setup(r => r.Load()).Returns(data);

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var result = manager.GetAllGoals().ToList();

        // Assert
        result.Should().HaveCount(3);
        result[0].Id.Should().Be("goal-3");
        result[2].Id.Should().Be("goal-1");
    }

    [Theory, AutoMoqData]
    public void DeactivateGoal_Should_Set_IsActive_False(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal { Id = "goal-to-deactivate", IsActive = true };
        data.Goals.Add(goal);

        repository.Setup(r => r.Load()).Returns(data);

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        manager.DeactivateGoal("goal-to-deactivate");

        // Assert
        goal.IsActive.Should().BeFalse();
        repository.Verify(r => r.Save(It.IsAny<GoalData>()), Times.Once);
    }

    [Theory, AutoMoqData]
    public void DeactivateGoal_Should_Do_Nothing_For_Invalid_Id(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        repository.Setup(r => r.Load()).Returns(new GoalData());

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        manager.DeactivateGoal("non-existent-id");

        // Assert
        repository.Verify(r => r.Save(It.IsAny<GoalData>()), Times.Never);
    }

    [Theory, AutoMoqData]
    public void GetGoalProgress_Should_Return_Progress_For_Active_Goal(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal { Id = "active-goal", IsActive = true, TargetWordCount = 500 };
        data.Goals.Add(goal);

        repository.Setup(r => r.Load()).Returns(data);

        var expectedProgress = new GoalProgressResult { CurrentWordCount = 200 };
        progressTracker
            .Setup(p => p.CalculateGoalProgress(data, goal))
            .Returns(expectedProgress);

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var result = manager.GetGoalProgress();

        // Assert
        result.Should().Be(expectedProgress);
    }

    [Theory, AutoMoqData]
    public void GetGoalProgress_Should_Return_Null_When_No_Active_Goal(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        repository.Setup(r => r.Load()).Returns(new GoalData());

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var result = manager.GetGoalProgress();

        // Assert
        result.Should().BeNull();
    }

    [Theory, AutoMoqData]
    public void RecordProgress_Should_Track_Progress_And_Update_Streak(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal { Id = "active-goal", IsActive = true, TargetWordCount = 500 };
        data.Goals.Add(goal);

        repository.Setup(r => r.Load()).Returns(data);

        progressTracker
            .Setup(p => p.CalculateGoalProgress(data, goal))
            .Returns(new GoalProgressResult { IsCompleted = false });

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        manager.RecordProgress(100, "chapter1.txt");

        // Assert
        progressTracker.Verify(p => p.RecordProgress(data, 100, goal.Id, "chapter1.txt"), Times.Once);
        streakCalculator.Verify(s => s.UpdateStreak(data), Times.Once);
        milestoneChecker.Verify(m => m.CheckForNewMilestones(data), Times.Once);
        repository.Verify(r => r.Save(data), Times.Once);
    }

    [Theory, AutoMoqData]
    public void RecordProgress_Should_Do_Nothing_When_No_Active_Goal(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        repository.Setup(r => r.Load()).Returns(new GoalData());

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        manager.RecordProgress(100);

        // Assert
        progressTracker.Verify(p => p.RecordProgress(It.IsAny<GoalData>(), It.IsAny<int>(),
            It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory, AutoMoqData]
    public void RecordProgress_Should_Increment_TotalGoalsCompleted_When_Goal_Completed(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var data = new GoalData { TotalGoalsCompleted = 0 };
        var goal = new Goal { Id = "active-goal", IsActive = true, TargetWordCount = 100 };
        data.Goals.Add(goal);

        repository.Setup(r => r.Load()).Returns(data);

        progressTracker
            .Setup(p => p.CalculateGoalProgress(data, goal))
            .Returns(new GoalProgressResult { IsCompleted = true });

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        manager.RecordProgress(100);

        // Assert
        data.TotalGoalsCompleted.Should().Be(1);
    }

    [Theory, AutoMoqData]
    public void GetGoalData_Should_Return_Current_Data(
        [Frozen] Mock<IGoalRepository> repository,
        [Frozen] Mock<IProgressTracker> progressTracker,
        [Frozen] Mock<IStreakCalculator> streakCalculator,
        [Frozen] Mock<IMilestoneChecker> milestoneChecker)
    {
        // Arrange
        var data = new GoalData { TotalWordsWritten = 1000 };
        repository.Setup(r => r.Load()).Returns(data);

        var manager = new GoalManager(repository.Object, progressTracker.Object,
            streakCalculator.Object, milestoneChecker.Object);

        // Act
        var result = manager.GetGoalData();

        // Assert
        result.Should().Be(data);
        result.TotalWordsWritten.Should().Be(1000);
    }
}
