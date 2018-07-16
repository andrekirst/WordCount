namespace WordCount.Interfaces
{
    public interface IStatisticsOutput
    {
        void WriteNumberOfWords(
            int numberOfWords,
            int maxCountOfFillingPoints);

        void WriteNumberOfUniqeWords(
            int numberOfUniqeWords,
            int maxCountOfFillingPoints);

        void WriteAveragewordLength(
            double averageWordLength,
            int maxCountOfFillingPoints);

        void WriteNumberOfChapters(
            int numberOfChapters,
            int maxCountOfFillingPoints);
    }
}