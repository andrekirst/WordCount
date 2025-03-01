using WordCount.Extensions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WordCount;

public interface IInteractor
{
    Task<int> Execute();
}

public class Interactor(
    ITextInput textInput,
    IWordCountAnalyzer wordCountAnalyzer,
    IWordCountAnalyzerOutput wordCountAnalyzerOutput,
    IIndexOutput indexOutput) : IInteractor
{
    public async Task<int> Execute()
    {
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
