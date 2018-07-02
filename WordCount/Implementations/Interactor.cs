using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class Interactor : IInteractor
    {
        private readonly ITextInput _textInput;
        private readonly IWordCountAnalyzer _wordCountAnalyzer;
        private readonly IWordCountAnalyzerOutput _wordCountAnalyzerOutput;
        private readonly IIndexOutput _indexOutput;

        public Interactor(
            ITextInput textInput,
            IWordCountAnalyzer wordCountAnalyzer,
            IWordCountAnalyzerOutput wordCountAnalyzerOutput,
            IIndexOutput indexOutput)
        {
            _textInput = textInput;
            _wordCountAnalyzer = wordCountAnalyzer;
            _wordCountAnalyzerOutput = wordCountAnalyzerOutput;
            _indexOutput = indexOutput;
        }

        public int Execute(string[] args)
        {
            try
            {
                string text = _textInput.GetInputText();

                WordCountAnalyzerResult analyzeResult = _wordCountAnalyzer.Analyze(text: text);

                _wordCountAnalyzerOutput.DisplayResult(wordCountAnalyzerResult: analyzeResult);

                // TODO Refactoring - Untergliederung
                //IndexOutputRequest indexOutputRequest = new IndexOutputRequest()
                //{
                //    DistinctWords = analyzeResult.DistinctWords,
                //    DictionaryTextFile = argumentsReaderResult.DictionaryTextFile,
                //    IsDictionaryParameterPresent = argumentsReaderResult.IsDictionaryParameterPresent
                //};

                //if (argumentsReaderResult.IsIndexParameterPresent)
                //{
                //    _indexOutput.OutputIndex(indexOutputRequest: indexOutputRequest); 
                //}
            }
            catch (System.Exception ex)
            {
                // TODO Ohne Exception-Handling den Fehlercode zurückgeben. Eventuell durch die rückgabe von komplexen Datentypen mit einem ReturnCode.
                return 1;
            }

            return 0;
        }
    }
}
