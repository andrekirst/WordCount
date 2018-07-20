using WordCount.Abstractions.SystemAbstractions.Reflection;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.Output
{
    public class HelpOutput : IHelpOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private readonly IHelpParameterParser _helpParameterParser;
        private readonly IAssembly _assembly;

        public HelpOutput(
            IDisplayOutput displayOutput,
            IHelpParameterParser helpParameterParser,
            IAssembly assembly)
        {
            _displayOutput = displayOutput;
            _helpParameterParser = helpParameterParser;
            _assembly = assembly;
        }

        public bool ShowHelpIfRequested()
        {
            HelpParameter helpParameter = _helpParameterParser.ParseHelpParameter();

            bool isPresent = helpParameter.IsPresent;

            if (isPresent)
            {
                _displayOutput.WriteLine(text: $"{_assembly.Name} - {_assembly.Version}");
                _displayOutput.WriteLine(text: string.Empty);
                _displayOutput.WriteLine(text: "-h | -help : Display this help");
                _displayOutput.WriteLine(text: "-index : Display the index of the analyzed Text");
                _displayOutput.WriteLine(text: "-dictionary=file : Uses the dictionary with the given file");
                _displayOutput.WriteLine(text: "-stopwordlist=file : Uses the stopword with the given file. Default: stopword.txt");
                _displayOutput.WriteLine(text: "-texturl=url : Takes the text file from an url");
                _displayOutput.WriteLine(text: "-lang=language : Supported languages: de, en. Default: en");
            }

            return helpParameter.IsPresent;
        }
    }
}
