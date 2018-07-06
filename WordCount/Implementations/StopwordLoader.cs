using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class StopwordLoader : IStopwordLoader
    {
        private const string StopwordFileName = "stopwords.txt";
        private readonly IFileSystem _fileSystem;
        private readonly IStopwordListParameterParser _stopwordListParameterParser;
        private readonly IDisplayOutput _displayOutput;

        public StopwordLoader(
            IFileSystem fileSystem,
            IStopwordListParameterParser stopwordListParameterParser,
            IDisplayOutput displayOutput)
        {
            _fileSystem = fileSystem;
            _stopwordListParameterParser = stopwordListParameterParser;
            _displayOutput = displayOutput;
        }

        public List<string> GetStopwords()
        {
            StopwordListParameter stopwordListParameter = _stopwordListParameterParser.ParseStopwordListParameter();

            bool isParameterPresent = stopwordListParameter.IsPresent;

            string fileName = isParameterPresent ? stopwordListParameter.FileName : StopwordFileName;

            if (!_fileSystem.File.Exists(path: fileName))
            {
                return new List<string>();
            }

            if (isParameterPresent)
            {
                _displayOutput.WriteLine(text: $"Used Stopwordlist: {fileName}"); 
            }

            return _fileSystem
                .File
                .ReadAllLines(path: fileName)
                .ToList();
        }
    }
}
