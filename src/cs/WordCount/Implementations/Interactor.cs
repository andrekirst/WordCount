using WordCount.Interfaces;
using WordCount.Extensions;
using WordCount.Interfaces.Output;
using WordCount.Models.Requests;
using System.Threading.Tasks;

namespace WordCount.Implementations;

public class Interactor(
    ITextInput textInput,
    IWordCountAnalyzer wordCountAnalyzer,
    IWordCountAnalyzerOutput wordCountAnalyzerOutput,
    IIndexOutput indexOutput,
    IHelpOutput helpOutput) : IInteractor
{
    public async Task<int> Execute()
    {
        var hasRequestedHelp = helpOutput.ShowHelpIfRequested();

        if (hasRequestedHelp)
        {
            return 1;
        }

        var inputTextResult = await textInput.GetInputText();

        if (inputTextResult.Text.IsNullOrEmpty())
        {
            return 0;
        }
        do
        {
            var analyzeResult = wordCountAnalyzer.Analyze(inputTextResult.Text);

            wordCountAnalyzerOutput.DisplayResult(analyzeResult);

            var indexOutputRequest = new IndexOutputRequest
            {
                DistinctWords = analyzeResult.DistinctWords
            };

            indexOutput.OutputIndex(indexOutputRequest);

            inputTextResult = await textInput.GetInputText();
        } while (inputTextResult.HasEnteredConsoleText);

        return 0;
    }
}
