using WordCount.Interfaces;
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

        public int Execute()
        {
            string text = _textInput.GetInputText();

            WordCountAnalyzerResult analyzeResult = _wordCountAnalyzer.Analyze(text: text);

            _wordCountAnalyzerOutput.DisplayResult(wordCountAnalyzerResult: analyzeResult);

            IndexOutputRequest indexOutputRequest = new IndexOutputRequest
            {
                DistinctWords = analyzeResult.DistinctWords
            };

            _indexOutput.OutputIndex(indexOutputRequest: indexOutputRequest);

            return 0;
        }
    }
}
