using WordCount.Interfaces;
using WordCount.Extensions;
using WordCount.Interfaces.Output;
using WordCount.Models.Requests;
using WordCount.Models.Results;

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
            bool hasRequestedHelp = HelpOutput.ShowHelpIfRequested();

            if (hasRequestedHelp)
            {
                return 1;
            }

            InputTextResult inputTextResult = TextInput.GetInputText();

            if (inputTextResult.Text.IsNullOrEmpty())
            {
                return 0;
            }
            do
            {
                WordCountAnalyzerResult analyzeResult = WordCountAnalyzer.Analyze(text: inputTextResult.Text);

                WordCountAnalyzerOutput.DisplayResult(result: analyzeResult);

                IndexOutputRequest indexOutputRequest = new IndexOutputRequest
                {
                    DistinctWords = analyzeResult.DistinctWords
                };

                IndexOutput.OutputIndex(indexOutputRequest: indexOutputRequest);

                inputTextResult = TextInput.GetInputText();
            } while (inputTextResult.HasEnteredConsoleText);

            return 0;
        }
    }
}
