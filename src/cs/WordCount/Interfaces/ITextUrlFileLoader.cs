using System.Threading.Tasks;

namespace WordCount.Interfaces
{
    public interface ITextUrlFileLoader
    {
        Task<string> ReadTextFile();
    }
}
