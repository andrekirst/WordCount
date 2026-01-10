# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

#### Word Count Goals Feature
- **Goal Management**: Set and track daily, weekly, or project-based word count goals
  - Daily goals for consistent daily writing habits
  - Weekly goals that reset each week
  - Project goals for long-term writing targets (e.g., NaNoWriMo 50K challenge)

- **Progress Tracking**: Visual progress visualization with detailed statistics
  - Progress bar showing percentage complete
  - Current word count vs target
  - Words remaining to reach goal
  - Average words per day calculation
  - Required words per day to complete on time

- **Streak Tracking**: Track consecutive days of writing activity
  - Current streak counter
  - Best/longest streak record
  - Last activity date tracking
  - Streak start date tracking

- **Achievement Milestones**: Earn milestones as you progress (13 types)
  - Goal milestones: First Goal Set, First Goal Completed, 5/10/25 Goals Completed
  - Streak milestones: 3-Day, 7-Day, 30-Day Streak
  - Word count milestones: 1K, 5K, 10K, 50K (NaNoWriMo), 100K Words

- **Command-Line Interface**:
  - `--goal-daily=N`: Create a daily goal of N words
  - `--goal-weekly=N`: Create a weekly goal of N words
  - `--goal-project=N`: Create a project goal of N words
  - `--project-name=NAME`: Set project name for project goals
  - `--goal-progress`: Display current goal progress
  - `--goal-stats`: Display statistics and milestones

- **Data Persistence**: Goals automatically saved to user's AppData directory
  - Windows: `%APPDATA%/WordCount/goals.json`
  - Linux/Mac: `~/.config/WordCount/goals.json`

- **New Interfaces and Classes**:
  - `IGoalManager` / `GoalManager` - Main entry point for goal operations
  - `IGoalRepository` / `JsonGoalRepository` - JSON file persistence
  - `IProgressTracker` / `ProgressTracker` - Progress calculation
  - `IStreakCalculator` / `StreakCalculator` - Streak management
  - `IMilestoneChecker` / `MilestoneChecker` - Milestone achievement detection
  - `IGoalOutput` / `GoalOutput` - Console output formatting
  - `IGoalParameterParser` / `GoalParameterParser` - CLI argument parsing

- **Models**:
  - `Goal` - Goal entity with type, target, dates, and progress entries
  - `GoalType` - Enum for Daily, Weekly, Project goal types
  - `ProgressEntry` - Individual progress record with date and word count
  - `Streak` - Streak tracking information
  - `Milestone` - Achievement milestone record
  - `MilestoneType` - Enum for all 13 milestone types
  - `GoalData` - Root container for all goal-related persistence
  - `GoalProgressResult` - Calculated progress information
  - `GoalParameter` - Parsed command-line parameters

- **Comprehensive Test Coverage**: 96+ new unit tests covering:
  - `GoalManagerTests` - Core goal management logic
  - `ProgressTrackerTests` - Progress calculation
  - `StreakCalculatorTests` - Streak logic
  - `MilestoneCheckerTests` - Milestone detection
  - `JsonGoalRepositoryTests` - Persistence layer
  - `GoalParameterParserTests` - CLI argument parsing
  - `GoalOutputTests` - Output formatting

- **Localization**: Resource strings for English and German
  - Goal creation success messages
  - Progress display labels
  - Milestone achievement messages
  - Error messages

#### Multi-File Analysis Feature
- **Batch Document Analysis**: Analyze multiple documents at once with folder scanning
  - Select a folder and file pattern to analyze all matching files
  - Recursive folder scanning to include subdirectories
  - Support for custom file patterns (e.g., `*.txt`, `*.md`)

- **Aggregate Statistics Dashboard**: Unified view of statistics across all files
  - Total word count across all files
  - Total unique words (case-insensitive deduplication)
  - Weighted average word length (proportional to file word counts)
  - Total chapter count and file size

- **Individual File Results**: Per-file breakdown alongside aggregates
  - Word count, unique words, and average word length per file
  - Chapter count and file size for each file
  - File name and path information

- **Command-Line Interface**:
  - `-f` or `--folder`: Specify the folder path to analyze
  - `-p` or `--pattern`: Specify the file pattern (default: `*.txt`)
  - Example: `WordCount -f /documents -p "*.md"`

- **LiveEditor Multi-File Page** (`/multi-file`):
  - Web-based interface for multi-file analysis
  - Add multiple documents by name and content
  - View aggregate statistics in a dashboard layout
  - Individual file results in a sortable table

- **New Classes and Interfaces**:
  - `IMultiFileAnalyzer` / `MultiFileAnalyzer` - Core folder analysis
  - `IMultiFileAnalyzerOutput` / `MultiFileAnalyzerOutput` - Dashboard display
  - `IFolderParameterParser` / `FolderParameterParser` - CLI argument parsing
  - `MultiFileAnalysisResult`, `FileAnalysisResult`, `AggregateStatistics` - Result models
  - `FolderParameter` - Command-line parameter model

- **Comprehensive Test Coverage**: 43 new unit tests covering:
  - `FolderParameterParserTests` - CLI argument parsing (12 tests)
  - `MultiFileAnalyzerTests` - Folder analysis logic (8 tests)
  - `MultiFileAnalyzerOutputTests` - Dashboard output (10 tests)
  - `MultiFileAnalysisServiceTests` - LiveEditor service (13 tests)

- **Localization**: Resource strings for English and German
  - Dashboard headers and labels
  - Error messages for missing folders and files

#### Live Text Editor Feature
- **Real-Time Text Analysis**: Web-based editor with live statistics updates as you type
  - Word count, character count (with/without spaces)
  - Sentence count, paragraph count, line count
  - Unique word count, average word length
  - Estimated reading time (based on 200 words/minute)

- **Blazor Web Application**: New `WordCount.LiveEditor` project
  - Built with Blazor Server for real-time SignalR communication
  - Responsive design with sidebar statistics panel
  - Status bar for quick metrics overview
  - CSS custom properties for easy theming

- **Services Architecture**:
  - `ITextStatisticsService` / `TextStatisticsService` - Core text analysis service
  - `IMultiFileAnalysisService` / `MultiFileAnalysisService` - Multi-document analysis
  - Full dependency injection support
  - Comprehensive XML documentation

- **Test Coverage**: 96+ unit tests for LiveEditor functionality
  - `TextStatisticsServiceTests` - Core service tests
  - `TextStatisticsEdgeCaseTests` - Edge cases and Unicode handling
  - `TextStatisticsModelTests` - Model validation tests
  - `MultiFileAnalysisServiceTests` - Multi-file service tests

#### Automatic Language Detection Feature
- **Language Detection**: Automatically detect the language of input text to apply appropriate word counting rules
  - Supports English (default), German, Chinese, Japanese, and Korean
  - Detection based on Unicode character analysis

- **Character-Based Word Counting**: For CJK (Chinese, Japanese, Korean) languages
  - Each CJK character is treated as an individual word
  - Mixed text (e.g., "Hello 世界") handles both Latin words and CJK characters correctly

- **New Interfaces and Classes**:
  - `ILanguageDetector` - Interface for language detection implementations
  - `ITextSplitStrategy` - Interface for text splitting strategies
  - `LanguageDetector` - Detects language from text content
  - `CharacterBasedTextSplitStrategy` - Splits CJK text by character
  - `SpaceBasedTextSplitStrategy` - Splits Western text by whitespace
  - `UnicodePatterns` - Centralized Unicode pattern constants and utilities
  - `LanguageDetectionResult` - Model for language detection results

- **Enhanced TextSplitResult**: Now includes `DetectedLanguage` and `IsCharacterBased` properties

- **Stopword Files**: Added stopword lists for Chinese, Japanese, and Korean languages
  - `stopwords.zh.txt` - Chinese stopwords
  - `stopwords.ja.txt` - Japanese stopwords
  - `stopwords.ko.txt` - Korean stopwords

- **LiveEditor Updates**:
  - New "Language Detection" panel showing detected language
  - Word counting mode indicator (Space-based/Character-based)
  - Data-testid attributes for Playwright testing

- **Comprehensive Test Coverage**: 99+ new unit tests covering:
  - Language detection for all supported languages
  - Character-based text splitting
  - Space-based text splitting
  - Integration tests for the complete flow
  - Edge cases (null, empty, whitespace inputs)

### Changed

- **TextSplit**: Now uses automatic language detection to choose the appropriate splitting strategy
- **LanguageCultureMappings**: Extended with Chinese (zh-CN), Japanese (ja-JP), and Korean (ko-KR) mappings
- **TextStatisticsService**: Updated to use language-aware text splitting

### Technical Improvements

- **DRY Principle**: Centralized Unicode patterns in `UnicodePatterns` helper class
- **Performance**: Pre-compiled regex patterns for better performance
- **Dependency Injection**: Full DI support in `TextSplit` class with multiple constructor overloads
- **XML Documentation**: Comprehensive XML documentation for all public APIs

## [Previous Versions]

For changes prior to the language detection feature, see the git commit history.
