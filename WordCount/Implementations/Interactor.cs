using WordCount.Interfaces;
using WordCount.Models.Requests;
using WordCount.Models.Results;

namespace WordCount.Implementations
{
    public class Interactor : IInteractor
    {
        private readonly ITextInput _textInput;
        private readonly IWordCountAnalyzer _wordCountAnalyzer;
        private readonly IWordCountAnalyzerOutput _wordCountAnalyzerOutput;
        private readonly IIndexOutput _indexOutput;
        private readonly IHelpOutput _helpOutput;

        public Interactor(
            ITextInput textInput,
            IWordCountAnalyzer wordCountAnalyzer,
            IWordCountAnalyzerOutput wordCountAnalyzerOutput,
            IIndexOutput indexOutput,
            IHelpOutput helpOutput)
        {
            _textInput = textInput;
            _wordCountAnalyzer = wordCountAnalyzer;
            _wordCountAnalyzerOutput = wordCountAnalyzerOutput;
            _indexOutput = indexOutput;
            _helpOutput = helpOutput;
        }

        public int Execute()
        {
            bool hasRequestedHelp = _helpOutput.ShowHelpIfRequested();

            if (hasRequestedHelp)
            {
                return 1;
            }

            InputTextResult inputTextResult = _textInput.GetInputText();
            do
            {
                WordCountAnalyzerResult analyzeResult = _wordCountAnalyzer.Analyze(text: inputTextResult.Text);

                _wordCountAnalyzerOutput.DisplayResult(wordCountAnalyzerResult: analyzeResult);

                IndexOutputRequest indexOutputRequest = new IndexOutputRequest
                {
                    DistinctWords = analyzeResult.DistinctWords
                };

                _indexOutput.OutputIndex(indexOutputRequest: indexOutputRequest);

                inputTextResult = _textInput.GetInputText();
            } while (inputTextResult.HasEnteredText);

            return 0;
        }
    }
}
