namespace WordCount.Abstractions.Console
{
    public interface IEnvironment
    {
        string[] GetCommandLineArgs();
    }
}
