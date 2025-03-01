namespace WordCount;

public interface IDisplayOutput
{
    void Write(string text);

    void WriteLine(string text);

    void WriteLine();

    void WriteErrorLine(string errorMessage);

    void WriteResource(string resourceIdent, params object[] placeholderValues);

    void WriteResourceLine(string resourceIdent, params object[] placeholderValues);

    void WriteErrorResourceLine(string resourceIdent, params object[] placeholderValues);
}
