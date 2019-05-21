using WordCount.Abstractions.SystemAbstractions.Reflection;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.Output
{
    public class HelpOutput : IHelpOutput
    {
        private IDisplayOutput DisplayOutput { get; }
        private IHelpParameterParser HelpParameterParser { get; }
        private IAssembly Assembly { get; }

        public HelpOutput(
            IDisplayOutput displayOutput,
            IHelpParameterParser helpParameterParser,
            IAssembly assembly)
        {
            DisplayOutput = displayOutput;
            HelpParameterParser = helpParameterParser;
            Assembly = assembly;
        }

        public bool ShowHelpIfRequested()
        {
            HelpParameter helpParameter = HelpParameterParser.ParseHelpParameter();

            bool isPresent = helpParameter.IsPresent;

            if (isPresent)
            {
                DisplayOutput.WriteLine(text: $"{Assembly.Name} - {Assembly.Version}");
                DisplayOutput.WriteLine(text: string.Empty);
                DisplayOutput.WriteLine(text: "-h | -help : Display this help");
                DisplayOutput.WriteLine(text: "-index : Display the index of the analyzed Text");
                DisplayOutput.WriteLine(text: "-dictionary=file : Uses the dictionary with the given file");
                DisplayOutput.WriteLine(text: "-stopwordlist=file : Uses the stopword with the given file. Default: stopword.txt");
                DisplayOutput.WriteLine(text: "-texturl=url : Takes the text file from an url");
                DisplayOutput.WriteLine(text: "-lang=language : Supported languages: de, en. Default: en");
            }

            return helpParameter.IsPresent;
        }
    }
}
