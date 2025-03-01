using System.Threading.Tasks;
using WordCount.Models.Results;

namespace WordCount.Interfaces
{
    public interface ITextInput
    {
        Task<InputTextResult> GetInputText();
    }
}
