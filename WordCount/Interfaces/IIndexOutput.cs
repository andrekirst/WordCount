using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface IIndexOutput
    {
        void OutputIndex(IndexOutputRequest indexOutputRequest);
    }
}
