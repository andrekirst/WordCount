using System;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class HelpOutput : IHelpOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private readonly IHelpParameterParser _helpParameterParser;

        public HelpOutput(
            IDisplayOutput displayOutput,
            IHelpParameterParser helpParameterParser)
        {
            _displayOutput = displayOutput;
            _helpParameterParser = helpParameterParser;
        }

        public bool ShowHelpIfRequested()
        {
            HelpParameter helpParameter = _helpParameterParser.ParseHelpParameter();

            return helpParameter.IsPresent;
        }
    }
}
