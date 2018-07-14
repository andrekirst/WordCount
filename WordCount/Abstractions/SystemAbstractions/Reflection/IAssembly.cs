namespace WordCount.Abstractions.SystemAbstractions.Reflection
{
    public interface IAssembly
    {
        string Name { get; }

        string Version { get; }
    }
}
