namespace WordCount.Abstractions.SystemAbstractions
{
    public interface IEnvironment
    {
        string[] GetCommandLineArgs();
    }
}