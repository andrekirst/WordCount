using System;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class Interactor : IInteractor
    {
        private readonly ITextInput _textInput;
        private readonly IWordCountAnalyzer _wordCountAnalyzer;
        private readonly IWordCountAnalyzerOutput _wordCountAnalyzerOutput;

        public Interactor(
            ITextInput textInput,
            IWordCountAnalyzer wordCountAnalyzer,
            IWordCountAnalyzerOutput wordCountAnalyzerOutput)
        {
            _textInput = textInput;
            _wordCountAnalyzer = wordCountAnalyzer;
            _wordCountAnalyzerOutput = wordCountAnalyzerOutput;
        }

        public int Execute()
        {
            Console.Write("Enter text: ");

            string text = _textInput.GetInputText();

            WordCountAnalyzerResult analyzeResult = _wordCountAnalyzer.Analyze(text: text);

            string displayResultAsString = _wordCountAnalyzerOutput.DisplayResultAsString(wordCountAnalyzerResult: analyzeResult);

            Console.WriteLine(displayResultAsString);

            return 0;
        }
    }
}
