using System;
using System.Reflection;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.Output;

public class HelpOutput(
    IDisplayOutput displayOutput,
    IParameterParser<HelpParameter> helpParameterParser) : IHelpOutput
{
    public bool ShowHelpIfRequested()
    {
        var assemblyName = Assembly.GetEntryAssembly().GetName();
        var args = Environment.GetCommandLineArgs();
        var helpParameter = helpParameterParser.ParseParameter(args);

        var isPresent = helpParameter.IsPresent;

        if (!isPresent) return helpParameter.IsPresent;
        
        displayOutput.WriteLine($"{assemblyName.Name} - {assemblyName.Version}");
        displayOutput.WriteLine(string.Empty);
        displayOutput.WriteLine("-h | -help : Display this help");
        displayOutput.WriteLine("-index : Display the index of the analyzed Text");
        displayOutput.WriteLine("-dictionary=file : Uses the dictionary with the given file");
        displayOutput.WriteLine("-stopwordlist=file : Uses the stopword with the given file. Default: stopword.txt");
        displayOutput.WriteLine("-texturl=url : Takes the text file from an url");
        displayOutput.WriteLine("-lang=language : Supported languages: de, en. Default: en");

        return helpParameter.IsPresent;
    }
}
