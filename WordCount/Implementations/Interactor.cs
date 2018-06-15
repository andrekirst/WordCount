using System;
using System.Collections.Generic;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class Interactor : IInteractor
    {
        private readonly ITextInput _textInput;
        private readonly IWordCountAnalyzer _wordCountAnalyzer;
        private readonly IWordCountAnalyzerOutput _wordCountAnalyzerOutput;
        private readonly IStopwordLoader _stopwordLoader;
        
        public Interactor(
            ITextInput textInput,
            IWordCountAnalyzer wordCountAnalyzer,
            IWordCountAnalyzerOutput wordCountAnalyzerOutput,
            IStopwordLoader stopwordLoader)
        {
            _textInput = textInput;
            _wordCountAnalyzer = wordCountAnalyzer;
            _wordCountAnalyzerOutput = wordCountAnalyzerOutput;
            _stopwordLoader = stopwordLoader;
        }

        public int Execute()
        {
            Console.Write("Enter text: ");

            string text = _textInput.GetInputText();

            List<string> stopwords = _stopwordLoader.GetStopwords();

            WordCountAnalyzerResult analyzeResult = _wordCountAnalyzer.Analyze(
                text: text,
                stopwords: stopwords);

            string displayResultAsString = _wordCountAnalyzerOutput.DisplayResultAsString(wordCountAnalyzerResult: analyzeResult);

            Console.WriteLine(value: displayResultAsString);

            return 0;
        }
    }
}
