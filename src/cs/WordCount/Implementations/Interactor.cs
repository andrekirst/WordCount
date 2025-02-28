using WordCount.Interfaces;
using WordCount.Extensions;
using WordCount.Interfaces.Output;
using WordCount.Models.Requests;

namespace WordCount.Implementations
{
    public class Interactor : IInteractor
    {
        private ITextInput TextInput { get; }
        private IWordCountAnalyzer WordCountAnalyzer { get; }
        private IWordCountAnalyzerOutput WordCountAnalyzerOutput { get; }
        private IIndexOutput IndexOutput { get; }
        private IHelpOutput HelpOutput { get; }

        public Interactor(
            ITextInput textInput,
            IWordCountAnalyzer wordCountAnalyzer,
            IWordCountAnalyzerOutput wordCountAnalyzerOutput,
            IIndexOutput indexOutput,
            IHelpOutput helpOutput)
        {
            TextInput = textInput;
            WordCountAnalyzer = wordCountAnalyzer;
            WordCountAnalyzerOutput = wordCountAnalyzerOutput;
            IndexOutput = indexOutput;
            HelpOutput = helpOutput;
        }

        public int Execute()
        {
            var hasRequestedHelp = HelpOutput.ShowHelpIfRequested();

            if (hasRequestedHelp)
            {
                return 1;
            }

            var inputTextResult = TextInput.GetInputText();

            if (inputTextResult.Text.IsNullOrEmpty())
            {
                return 0;
            }
            do
            {
                var analyzeResult = WordCountAnalyzer.Analyze(inputTextResult.Text);

                WordCountAnalyzerOutput.DisplayResult(analyzeResult);

                var indexOutputRequest = new IndexOutputRequest
                {
                    DistinctWords = analyzeResult.DistinctWords
                };

                IndexOutput.OutputIndex(indexOutputRequest);

                inputTextResult = TextInput.GetInputText();
            } while (inputTextResult.HasEnteredConsoleText);

            return 0;
        }
    }
}
