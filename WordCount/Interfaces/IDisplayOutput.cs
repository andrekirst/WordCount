namespace WordCount.Interfaces
{
    public interface IDisplayOutput
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteErrorLine(string errorMessage);
    }
}
