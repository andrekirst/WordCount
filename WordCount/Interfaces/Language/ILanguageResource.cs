namespace WordCount.Interfaces.Language
{
    public interface ILanguageResource
    {
        string GetResourceStringById(string resourceIdent);

        int DetectLongestResourceString(string[] resourceIdents);
    }
}