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
        private readonly IIndexOutput _indexOutput;

        public Interactor(
            ITextInput textInput,
            IWordCountAnalyzer wordCountAnalyzer,
            IWordCountAnalyzerOutput wordCountAnalyzerOutput,
            IArgumentsReader argumentsReader,
            IDisplayOutput displayOutput,
            IIndexOutput indexOutput)
        {
            _textInput = textInput;
            _wordCountAnalyzer = wordCountAnalyzer;
            _wordCountAnalyzerOutput = wordCountAnalyzerOutput;
            _argumentsReader = argumentsReader;
            _displayOutput = displayOutput;
            _indexOutput = indexOutput;
        }

        public int Execute(string[] args)
        {
            try
            {
                ArgumentsReaderResult argumentsReaderResult = _argumentsReader.ReadArguments(args: args);

                if (!argumentsReaderResult.IsSourceTextFileParameterPresent)
                {
                    _displayOutput.Write(text: "Enter text: ");
                }

                string text = _textInput.GetInputText(argumentsReaderResult: argumentsReaderResult);

                WordCountAnalyzerResult analyzeResult = _wordCountAnalyzer.Analyze(text: text);

                string displayResultAsString = _wordCountAnalyzerOutput.DisplayResultAsString(wordCountAnalyzerResult: analyzeResult);

                _displayOutput.WriteLine(text: displayResultAsString);

                IndexOutputRequest indexOutputRequest = new IndexOutputRequest()
                {
                    DistinctWords = analyzeResult.DistinctWords,
                    DictionaryTextFile = argumentsReaderResult.DictionaryTextFile,
                    IsDictionaryParameterPresent = argumentsReaderResult.IsDictionaryParameterPresent
                };

                if (argumentsReaderResult.IsIndexParameterPresent)
                {
                    _indexOutput.OutputIndex(indexOutputRequest: indexOutputRequest); 
                }
            }
            catch (System.Exception)
            {
                return 1;
            }

            return 0;
        }
    }
}
