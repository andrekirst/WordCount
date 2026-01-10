using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;

namespace WordCount.Implementations
{
    /// <summary>
    /// Analyzes multiple text files in a folder and provides aggregate statistics.
    /// </summary>
    /// <remarks>
    /// This class enables batch analysis of text documents, providing both individual
    /// file statistics and aggregated metrics across all analyzed files. It supports
    /// recursive folder scanning with customizable file patterns.
    /// </remarks>
    public class MultiFileAnalyzer : IMultiFileAnalyzer
    {
        private IFileSystem FileSystem { get; }
        private IWordCountAnalyzer WordCountAnalyzer { get; }
        private IDisplayOutput DisplayOutput { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiFileAnalyzer"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system abstraction for file operations.</param>
        /// <param name="wordCountAnalyzer">The analyzer for individual file word counting.</param>
        /// <param name="displayOutput">The output handler for error messages.</param>
        public MultiFileAnalyzer(
            IFileSystem fileSystem,
            IWordCountAnalyzer wordCountAnalyzer,
            IDisplayOutput displayOutput)
        {
            FileSystem = fileSystem;
            WordCountAnalyzer = wordCountAnalyzer;
            DisplayOutput = displayOutput;
        }

        /// <summary>
        /// Analyzes all files matching the specified pattern in a folder and its subfolders.
        /// </summary>
        /// <param name="folderPath">The path to the folder to analyze.</param>
        /// <param name="filePattern">The file pattern to match (e.g., "*.txt", "*.md").</param>
        /// <returns>
        /// A <see cref="MultiFileAnalysisResult"/> containing individual file results and aggregate statistics.
        /// If the folder doesn't exist or contains no matching files, returns an empty result with appropriate error messages.
        /// </returns>
        /// <remarks>
        /// The analysis includes:
        /// <list type="bullet">
        /// <item><description>Word count, unique words, and average word length per file</description></item>
        /// <item><description>Chapter count per file</description></item>
        /// <item><description>File size in bytes</description></item>
        /// <item><description>Aggregated totals across all files</description></item>
        /// <item><description>Weighted average word length calculation</description></item>
        /// </list>
        /// Files that cannot be read are skipped with an error message, and the analysis continues with remaining files.
        /// </remarks>
        public MultiFileAnalysisResult AnalyzeFolder(string folderPath, string filePattern)
        {
            var result = new MultiFileAnalysisResult
            {
                FolderPath = folderPath
            };

            if (!FileSystem.Directory.Exists(folderPath))
            {
                DisplayOutput.WriteErrorResourceLine("FOLDER_NOT_FOUND", folderPath);
                return result;
            }

            var files = FileSystem.Directory.GetFiles(folderPath, filePattern, System.IO.SearchOption.AllDirectories);

            if (files.Length == 0)
            {
                DisplayOutput.WriteErrorResourceLine("NO_FILES_FOUND", filePattern, folderPath);
                return result;
            }

            var fileResults = new List<FileAnalysisResult>();

            // Use case-insensitive HashSet to track unique words across all files
            var allDistinctWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Accumulators for aggregate statistics
            var totalWords = 0;
            var totalChapters = 0;
            long totalFileSize = 0;

            // Variables for calculating weighted average word length
            // Formula: Σ(avgWordLength_i × wordCount_i) / Σ(wordCount_i)
            var wordLengthSum = 0.0;
            var wordCount = 0;

            foreach (var filePath in files)
            {
                try
                {
                    var fileInfo = FileSystem.FileInfo.FromFileName(filePath);
                    var text = FileSystem.File.ReadAllText(filePath)
                        .Replace($"-{Environment.NewLine}", string.Empty);

                    var analysisResult = WordCountAnalyzer.Analyze(text);

                    var fileResult = new FileAnalysisResult
                    {
                        FileName = FileSystem.Path.GetFileName(filePath),
                        FilePath = filePath,
                        FileSizeBytes = fileInfo.Length,
                        AnalysisResult = analysisResult
                    };

                    fileResults.Add(fileResult);

                    // Aggregate statistics
                    totalWords += analysisResult.NumberOfWords;
                    totalChapters += analysisResult.NumberOfChapters;
                    totalFileSize += fileInfo.Length;

                    if (analysisResult.DistinctWords != null)
                    {
                        foreach (var word in analysisResult.DistinctWords)
                        {
                            allDistinctWords.Add(word);
                        }
                    }

                    // Calculate weighted average word length
                    if (analysisResult.NumberOfWords > 0)
                    {
                        wordLengthSum += analysisResult.AverageWordLength * analysisResult.NumberOfWords;
                        wordCount += analysisResult.NumberOfWords;
                    }
                }
                catch (Exception ex)
                {
                    DisplayOutput.WriteErrorResourceLine("FILE_READ_ERROR", filePath, ex.Message);
                }
            }

            result.FileResults = fileResults;
            result.TotalFilesAnalyzed = fileResults.Count;

            result.AggregateStatistics = new AggregateStatistics
            {
                TotalWords = totalWords,
                TotalUniqueWords = allDistinctWords.Count,
                AverageWordLength = wordCount > 0 ? wordLengthSum / wordCount : 0.0,
                TotalChapters = totalChapters,
                AllDistinctWords = allDistinctWords.OrderBy(w => w).ToList(),
                TotalFileSizeBytes = totalFileSize
            };

            return result;
        }
    }
}
