namespace WordCount.Abstractions.Console
{
    public interface IConsole
    {
        string ReadLine();

        void Write(string text);

        void WriteLine(string text);
    }
}
