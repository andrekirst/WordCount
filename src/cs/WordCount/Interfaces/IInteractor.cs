using System.Threading.Tasks;

namespace WordCount.Interfaces
{
    public interface IInteractor
    {
        Task<int> Execute();
    }
}
