using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class Interactor : IInteractor
    {
        private readonly ITextInput _textInput;
        private readonly IWordCountAnalyzer _wordCountAnalyzer;
        private readonly IWordCountAnalyzerOutput _wordCountAnalyzerOutput;
        private readonly IArgumentsReader _argumentsReader;
        private readonly IDisplayOutput _displayOutput;

        public Interactor(
            ITextInput textInput,
            IWordCountAnalyzer wordCountAnalyzer,
            IWordCountAnalyzerOutput wordCountAnalyzerOutput,
            IArgumentsReader argumentsReader,
            IDisplayOutput displayOutput)
        {
            _textInput = textInput;
            _wordCountAnalyzer = wordCountAnalyzer;
            _wordCountAnalyzerOutput = wordCountAnalyzerOutput;
            _argumentsReader = argumentsReader;
            _displayOutput = displayOutput;
        }

        public int Execute(string[] args)
        {
            ArgumentsReaderResult argumentsReaderResult = _argumentsReader.ReadArguments(args: args);

            if (!argumentsReaderResult.IsSourceTextFilePresent)
            {
                _displayOutput.Write(text: "Enter text: "); 
            }

            string text = _textInput.GetInputText(argumentsReaderResult: argumentsReaderResult);

            WordCountAnalyzerResult analyzeResult = _wordCountAnalyzer.Analyze(text: text);

            string displayResultAsString = _wordCountAnalyzerOutput.DisplayResultAsString(wordCountAnalyzerResult: analyzeResult);

            _displayOutput.WriteLine(text: displayResultAsString);

            return 0;
        }
    }
}
