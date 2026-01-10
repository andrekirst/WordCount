using System;
using System.IO;
using System.IO.Abstractions;
using System.Text.Json;
using WordCount.Interfaces.Goals;
using WordCount.Models.Goals;

namespace WordCount.Implementations.Goals
{
    public class GoalRepository : IGoalRepository
    {
        private const string AppFolderName = "WordCount";
        private const string GoalDataFileName = "goals.json";

        private readonly IFileSystem _fileSystem;
        private readonly string _dataFilePath;
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public GoalRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            var appDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var wordCountFolder = _fileSystem.Path.Combine(appDataPath, AppFolderName);

            EnsureDirectoryExists(wordCountFolder);

            _dataFilePath = _fileSystem.Path.Combine(wordCountFolder, GoalDataFileName);
        }

        public GoalData Load()
        {
            if (!_fileSystem.File.Exists(_dataFilePath))
            {
                return new GoalData();
            }

            try
            {
                var json = _fileSystem.File.ReadAllText(_dataFilePath);
                return JsonSerializer.Deserialize<GoalData>(json, JsonOptions) ?? new GoalData();
            }
            catch (JsonException)
            {
                // JSON parsing failed - return fresh data
                return new GoalData();
            }
            catch (IOException)
            {
                // File access issues - return fresh data
                return new GoalData();
            }
        }

        public void Save(GoalData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var json = JsonSerializer.Serialize(data, JsonOptions);
            _fileSystem.File.WriteAllText(_dataFilePath, json);
        }

        private void EnsureDirectoryExists(string path)
        {
            if (!_fileSystem.Directory.Exists(path))
            {
                _fileSystem.Directory.CreateDirectory(path);
            }
        }
    }
}
