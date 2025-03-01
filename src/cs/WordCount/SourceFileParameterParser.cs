using System.Linq;
using WordCount.Extensions;
using WordCount.Models.Parameters;

namespace WordCount;

public class SourceFileParameterParser : BaseParameterParser<SourceFileParameter>, IParameterParser<SourceFileParameter>
{
    public SourceFileParameter ParseParameter(string[] args)
    {
        return CachedValue(() =>
        {
            var fileName = args.FirstOrDefault(s => !s.StartsWith("-")) ?? string.Empty;

            var isPresent = fileName.IsFilled();

            return new SourceFileParameter
            {
                IsPresent = isPresent,
                FileName = fileName
            };
        });
    }
}
