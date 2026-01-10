# WordCount

## Badges

[![Black Duck Security Risk](https://copilot.blackducksoftware.com/github/repos/andrekirst/WordCount/branches/master/badge-risk.svg)](https://copilot.blackducksoftware.com/github/repos/andrekirst/WordCount/branches/master) [![codecov](https://codecov.io/gh/andrekirst/WordCount/branch/master/graph/badge.svg)](https://codecov.io/gh/andrekirst/WordCount) [![Build status](https://ci.appveyor.com/api/projects/status/4fny54mf5sjr5l8n/branch/master?svg=true)](https://ci.appveyor.com/project/andrekirst/wordcount/branch/master) [![BCH compliance](https://bettercodehub.com/edge/badge/andrekirst/WordCount?branch=master)](https://bettercodehub.com/) [![CodeFactor](https://www.codefactor.io/repository/github/andrekirst/wordcount/badge)](https://www.codefactor.io/repository/github/andrekirst/wordcount) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/d885b1f91d0542b2aca6d27689cc1e62)](https://www.codacy.com/app/github_85/WordCount?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=andrekirst/WordCount&amp;utm_campaign=Badge_Grade)

### [![SonarQube](https://sonarcloud.io/images/project_badges/sonarcloud-orange.svg)](https://sonarcloud.io/images/project_badges/sonarcloud-orange.svg)

[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=alert_status)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=alert_status)
[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=bugs)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=bugs)
[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=code_smells)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=code_smells)
[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=coverage)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=coverage)
[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=ncloc)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=ncloc)
[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=sqale_rating)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=sqale_rating)
[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=reliability_rating)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=reliability_rating)
[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=sqale_index)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=sqale_index)
[![SonarQube](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=vulnerabilities)](https://sonarcloud.io/api/project_badges/measure?project=andrekirst%3AWordCount&metric=vulnerabilities)

## Description

WordCount-Kata

See [http://ccd-school.de/coding-dojo/#cd8](http://ccd-school.de/coding-dojo/#cd8)

## Features

### Automatic Language Detection

WordCount automatically detects the language of input text and applies appropriate word counting rules:

| Language | Detection Method | Word Counting Mode |
|----------|------------------|-------------------|
| English | Default | Space-based |
| German | Umlauts (ä, ö, ü) and eszett (ß) | Space-based |
| Chinese | CJK Unified Ideographs | Character-based |
| Japanese | Hiragana (あ-ん) and Katakana (ア-ン) | Character-based |
| Korean | Hangul syllables | Character-based |

**Space-based counting**: Words are separated by whitespace. Example: "Hello world" = 2 words

**Character-based counting**: Each CJK character counts as one word. Example: "你好世界" = 4 words

#### Usage Example

```csharp
using WordCount.Implementations;

var textSplit = new TextSplit();

// English text
var englishResult = textSplit.Split("Hello world");
// Result: Words = ["Hello", "world"], DetectedLanguage = "en", IsCharacterBased = false

// Chinese text
var chineseResult = textSplit.Split("你好世界");
// Result: Words = ["你", "好", "世", "界"], DetectedLanguage = "zh", IsCharacterBased = true

// Mixed text
var mixedResult = textSplit.Split("Hello 世界");
// Result: Words = ["Hello", "世", "界"], DetectedLanguage = "zh", IsCharacterBased = true
```

### Multi-File Analysis

WordCount supports analyzing multiple documents at once, providing aggregate statistics and individual file results in a unified dashboard view.

#### Console Usage

Analyze all text files in a folder:

```bash
# Analyze all .txt files in a folder
WordCount -f /path/to/documents

# Analyze specific file types with custom pattern
WordCount -f /path/to/documents -p "*.md"

# Analyze all markdown files
WordCount --folder /my/writings --pattern "*.md"
```

#### Command-Line Arguments

| Argument | Description |
|----------|-------------|
| `-f` or `--folder` | Path to the folder containing files to analyze |
| `-p` or `--pattern` | File pattern to match (default: `*.txt`) |

#### Output Dashboard

The multi-file analysis displays a comprehensive dashboard:

```
Multi-File Analysis Dashboard
============================================================
Folder analyzed: /path/to/documents
Total files analyzed: 3

Aggregate Statistics:
----------------------------------------
- Total words..................... 1,250
- Total unique words.............. 487
- Overall average word length..... 5.2 Characters
- Total chapters.................. 5
- Total file size................. 45.2 KB

Individual File Results:
----------------------------------------

File: chapter1.txt
  - Number of words............... 450
  - Unique words.................. 210
  - Average word length........... 5.1 Characters
  - Chapters...................... 2
  - File size..................... 15.1 KB

[... more files ...]
============================================================
```

### Word Count Goals

Set and track daily, weekly, or project-based word count goals with progress visualization, streak tracking, and achievement milestones. Perfect for writers, novelists, and content creators who want to maintain productivity.

#### Creating Goals

```bash
# Create a daily goal of 500 words
WordCount --goal-daily=500

# Create a weekly goal of 3,500 words
WordCount --goal-weekly=3500

# Create a project goal with a name
WordCount --goal-project=50000 --project-name="My Novel"
```

#### Viewing Progress

```bash
# View current goal progress with streak and chart
WordCount --goal-progress

# View all statistics and milestones
WordCount --goal-stats
```

#### Command-Line Arguments

| Argument | Description |
|----------|-------------|
| `--goal-daily=N` | Create a daily goal of N words |
| `--goal-weekly=N` | Create a weekly goal of N words |
| `--goal-project=N` | Create a project goal of N words |
| `--project-name=NAME` | Set project name for project goals |
| `--goal-progress` | Display current goal progress |
| `--goal-stats` | Display statistics and milestones |

#### Progress Output

When you have an active goal, WordCount displays progress visualization:

```
Goal Progress
==================================================
  Daily Goal: 500 words

  [###############---------------] 50.0%

  Current: 250 / 500 words
  Remaining: 250 words
  Avg words/day: 250.0
==================================================

Streak Information
------------------------------
  [**] Current streak: 3 days
  Best streak: 5 days
  Last activity: Jan 10, 2026
```

#### Achievement Milestones

Earn milestones as you progress:

| Category | Milestones |
|----------|------------|
| **Goals** | First Goal Set, First Goal Completed, 5/10/25 Goals Completed |
| **Streaks** | 3-Day, 7-Day, 30-Day Streak |
| **Word Count** | 1K, 5K, 10K, 50K (NaNoWriMo), 100K Words |

#### Data Storage

Goal data is automatically persisted to:
- **Windows**: `%APPDATA%/WordCount/goals.json`
- **Linux/Mac**: `~/.config/WordCount/goals.json`

### LiveEditor

The WordCount.LiveEditor project provides a web-based interface for real-time text analysis:

- Live word count as you type
- Character count (with and without spaces)
- Sentence and paragraph counting
- Unique word count and average word length
- Estimated reading time
- **Automatic language detection display**
- **Word counting mode indicator (Space-based/Character-based)**
- **Multi-file analysis dashboard** (at `/multi-file` route)

#### LiveEditor Architecture

```
WordCount.LiveEditor/
├── Components/
│   ├── Pages/
│   │   └── LiveEditor.razor       # Main editor component
│   └── Layout/
│       ├── MainLayout.razor       # App layout
│       └── NavMenu.razor          # Navigation menu
├── Services/
│   ├── TextStatistics.cs          # Text analysis service
│   └── MultiFileAnalysisService.cs # Multi-file analysis
└── Program.cs                      # Service registration
```

#### CSS Theming

The LiveEditor supports CSS custom properties for easy theming:

```css
:root {
    --editor-primary-color: #4361ee;
    --editor-heading-color: #1a1a2e;
    --editor-text-color: #555;
    --editor-background: #ffffff;
    --editor-border-color: #e0e0e0;
    /* ... more variables available */
}
```

## Projects

| Project | Description |
|---------|-------------|
| `WordCount` | Core library with word counting logic and language detection |
| `WordCount.Tests` | Unit tests for the core library |
| `WordCount.LiveEditor` | Blazor web application for live text analysis |
| `WordCount.LiveEditor.Tests` | Tests for the LiveEditor application |

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Node.js (for Playwright tests)

### Building

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Running the LiveEditor

```bash
dotnet run --project WordCount.LiveEditor
```
