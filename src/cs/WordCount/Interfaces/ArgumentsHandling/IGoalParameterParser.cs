using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IGoalParameterParser
    {
        GoalParameter ParseGoalParameter();
    }
}
