using WordCount.Models.Requests;

namespace WordCount.Interfaces.Output
{
    public interface IIndexOutput
    {
        void OutputIndex(IndexOutputRequest indexOutputRequest);
    }
}
