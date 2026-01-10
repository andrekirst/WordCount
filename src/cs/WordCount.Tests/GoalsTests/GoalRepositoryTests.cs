using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using WordCount.Implementations.Goals;
using WordCount.Models.Goals;
using Xunit;

namespace WordCount.Tests.GoalsTests;

public class GoalRepositoryTests
{
    private readonly MockFileSystem _fileSystem;
    private readonly GoalRepository _sut;
    private readonly string _expectedDataFilePath;

    public GoalRepositoryTests()
    {
        _fileSystem = new MockFileSystem();
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var wordCountFolder = _fileSystem.Path.Combine(appDataPath, "WordCount");
        _expectedDataFilePath = _fileSystem.Path.Combine(wordCountFolder, "goals.json");

        _sut = new GoalRepository(_fileSystem);
    }

    [Fact]
    public void Constructor_Should_Create_WordCount_Directory_If_Not_Exists()
    {
        // Arrange
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var wordCountFolder = _fileSystem.Path.Combine(appDataPath, "WordCount");

        // Assert
        _fileSystem.Directory.Exists(wordCountFolder).Should().BeTrue();
    }

    [Fact]
    public void Load_Should_Return_Empty_GoalData_When_File_Does_Not_Exist()
    {
        // Act
        var result = _sut.Load();

        // Assert
        result.Should().NotBeNull();
        result.Goals.Should().BeEmpty();
        result.ProgressEntries.Should().BeEmpty();
        result.Milestones.Should().BeEmpty();
        result.TotalWordsWritten.Should().Be(0);
        result.TotalGoalsCompleted.Should().Be(0);
    }

    [Fact]
    public void Save_Should_Create_File_With_GoalData()
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { TargetWordCount = 1000, Type = GoalType.Daily });
        data.TotalWordsWritten = 500;

        // Act
        _sut.Save(data);

        // Assert
        _fileSystem.File.Exists(_expectedDataFilePath).Should().BeTrue();
        var content = _fileSystem.File.ReadAllText(_expectedDataFilePath);
        content.Should().Contain("1000");
        content.Should().Contain("500");
    }

    [Fact]
    public void Load_Should_Return_Saved_GoalData()
    {
        // Arrange
        var data = new GoalData();
        var goal = new Goal
        {
            Id = "test-goal-id",
            TargetWordCount = 2000,
            Type = GoalType.Weekly,
            ProjectName = "TestProject"
        };
        data.Goals.Add(goal);
        data.TotalWordsWritten = 1500;
        data.TotalGoalsCompleted = 3;
        _sut.Save(data);

        // Act
        var result = _sut.Load();

        // Assert
        result.Should().NotBeNull();
        result.Goals.Should().ContainSingle();
        result.Goals[0].Id.Should().Be("test-goal-id");
        result.Goals[0].TargetWordCount.Should().Be(2000);
        result.Goals[0].Type.Should().Be(GoalType.Weekly);
        result.Goals[0].ProjectName.Should().Be("TestProject");
        result.TotalWordsWritten.Should().Be(1500);
        result.TotalGoalsCompleted.Should().Be(3);
    }

    [Fact]
    public void Load_Should_Return_Empty_GoalData_On_Invalid_Json()
    {
        // Arrange
        _fileSystem.File.WriteAllText(_expectedDataFilePath, "invalid json content {{{");

        // Act
        var result = _sut.Load();

        // Assert
        result.Should().NotBeNull();
        result.Goals.Should().BeEmpty();
    }

    [Fact]
    public void Save_And_Load_Should_Preserve_ProgressEntries()
    {
        // Arrange
        var data = new GoalData();
        var entry = new ProgressEntry
        {
            Id = "entry-1",
            GoalId = "goal-1",
            WordCount = 250,
            SourceFile = "chapter1.txt"
        };
        data.ProgressEntries.Add(entry);
        _sut.Save(data);

        // Act
        var result = _sut.Load();

        // Assert
        result.ProgressEntries.Should().ContainSingle();
        result.ProgressEntries[0].Id.Should().Be("entry-1");
        result.ProgressEntries[0].GoalId.Should().Be("goal-1");
        result.ProgressEntries[0].WordCount.Should().Be(250);
        result.ProgressEntries[0].SourceFile.Should().Be("chapter1.txt");
    }

    [Fact]
    public void Save_And_Load_Should_Preserve_Milestones()
    {
        // Arrange
        var data = new GoalData();
        var milestone = new Milestone
        {
            Type = MilestoneType.FirstGoalSet,
            Description = "First goal!",
            IsNew = false
        };
        data.Milestones.Add(milestone);
        _sut.Save(data);

        // Act
        var result = _sut.Load();

        // Assert
        result.Milestones.Should().ContainSingle();
        result.Milestones[0].Type.Should().Be(MilestoneType.FirstGoalSet);
        result.Milestones[0].Description.Should().Be("First goal!");
        result.Milestones[0].IsNew.Should().BeFalse();
    }

    [Fact]
    public void Save_And_Load_Should_Preserve_Streak()
    {
        // Arrange
        var data = new GoalData();
        data.CurrentStreak = new Streak
        {
            CurrentStreak = 5,
            LongestStreak = 10,
            LastActivityDate = DateTime.UtcNow.Date,
            StreakStartDate = DateTime.UtcNow.Date.AddDays(-4)
        };
        _sut.Save(data);

        // Act
        var result = _sut.Load();

        // Assert
        result.CurrentStreak.CurrentStreak.Should().Be(5);
        result.CurrentStreak.LongestStreak.Should().Be(10);
    }

    [Fact]
    public void Save_Should_Overwrite_Existing_File()
    {
        // Arrange
        var data1 = new GoalData { TotalWordsWritten = 100 };
        _sut.Save(data1);

        var data2 = new GoalData { TotalWordsWritten = 500 };

        // Act
        _sut.Save(data2);
        var result = _sut.Load();

        // Assert
        result.TotalWordsWritten.Should().Be(500);
    }

    [Fact]
    public void Save_And_Load_Should_Preserve_Multiple_Goals()
    {
        // Arrange
        var data = new GoalData();
        data.Goals.Add(new Goal { Id = "goal-1", Type = GoalType.Daily, TargetWordCount = 500 });
        data.Goals.Add(new Goal { Id = "goal-2", Type = GoalType.Weekly, TargetWordCount = 3500 });
        data.Goals.Add(new Goal { Id = "goal-3", Type = GoalType.Project, TargetWordCount = 50000 });
        _sut.Save(data);

        // Act
        var result = _sut.Load();

        // Assert
        result.Goals.Should().HaveCount(3);
    }
}
