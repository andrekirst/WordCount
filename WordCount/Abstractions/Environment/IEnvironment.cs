namespace WordCount.Abstractions.Environment
{
    public interface IEnvironment
    {
        string[] GetCommandLineArgs();
    }
}