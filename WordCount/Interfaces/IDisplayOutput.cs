namespace WordCount.Interfaces
{
    public interface IDisplayOutput
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteLine();

        void WriteErrorLine(string errorMessage);

        void WriteResourceStringWithValues(string resourceIdent, params object[] values);

        void WriteResourceStringWithValuesLine(string resourceIdent, params object[] values);

        void WriteErrorResourceStringWithValuesLine(string resourceIdent, params object[] values);
    }
}
