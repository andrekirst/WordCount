using WordCount.Abstractions.SystemAbstractions.Reflection;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;

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
            var helpParameter = HelpParameterParser.ParseHelpParameter();

            var isPresent = helpParameter.IsPresent;

            if (!isPresent) return helpParameter.IsPresent;
            
            DisplayOutput.WriteLine($"{Assembly.Name} - {Assembly.Version}");
            DisplayOutput.WriteLine(string.Empty);
            DisplayOutput.WriteLine("-h | -help : Display this help");
            DisplayOutput.WriteLine("-index : Display the index of the analyzed Text");
            DisplayOutput.WriteLine("-dictionary=file : Uses the dictionary with the given file");
            DisplayOutput.WriteLine("-stopwordlist=file : Uses the stopword with the given file. Default: stopword.txt");
            DisplayOutput.WriteLine("-texturl=url : Takes the text file from an url");
            DisplayOutput.WriteLine("-lang=language : Supported languages: de, en. Default: en");

            return helpParameter.IsPresent;
        }
    }
}
