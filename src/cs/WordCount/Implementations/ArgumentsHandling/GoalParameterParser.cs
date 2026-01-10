using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Goals;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class GoalParameterParser : BaseParameterParser<GoalParameter>, IGoalParameterParser
    {
        // Command-line argument prefixes
        private const string DailyGoalPrefix = "--goal-daily=";
        private const string WeeklyGoalPrefix = "--goal-weekly=";
        private const string ProjectGoalPrefix = "--goal-project=";
        private const string ProjectNamePrefix = "--project-name=";
        private const string GoalProgressFlag = "--goal-progress";
        private const string GoalStatsFlag = "--goal-stats";

        private readonly IEnvironment _environment;

        public GoalParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public GoalParameter ParseGoalParameter()
        {
            return CachedValue(() =>
            {
                var args = _environment.GetCommandLineArgs() ?? Array.Empty<string>();
                var result = new GoalParameter();

                // Check for --goal-daily=N
                var dailyGoal = args.FirstOrDefault(a => a.StartsWith(DailyGoalPrefix, StringComparison.OrdinalIgnoreCase));
                if (dailyGoal != null)
                {
                    result.IsPresent = true;
                    result.GoalType = GoalType.Daily;
                    result.TargetWordCount = ParseWordCount(dailyGoal);
                }

                // Check for --goal-weekly=N
                var weeklyGoal = args.FirstOrDefault(a => a.StartsWith(WeeklyGoalPrefix, StringComparison.OrdinalIgnoreCase));
                if (weeklyGoal != null)
                {
                    result.IsPresent = true;
                    result.GoalType = GoalType.Weekly;
                    result.TargetWordCount = ParseWordCount(weeklyGoal);
                }

                // Check for --goal-project=N
                var projectGoal = args.FirstOrDefault(a => a.StartsWith(ProjectGoalPrefix, StringComparison.OrdinalIgnoreCase));
                if (projectGoal != null)
                {
                    result.IsPresent = true;
                    result.GoalType = GoalType.Project;
                    result.TargetWordCount = ParseWordCount(projectGoal);
                }

                // Check for --project-name=X
                var projectName = args.FirstOrDefault(a => a.StartsWith(ProjectNamePrefix, StringComparison.OrdinalIgnoreCase));
                if (projectName != null)
                {
                    result.ProjectName = projectName.Substring(ProjectNamePrefix.Length);
                }

                // Check for --goal-progress flag
                result.ShowProgress = args.Any(a => a.Equals(GoalProgressFlag, StringComparison.OrdinalIgnoreCase));

                // Check for --goal-stats flag
                result.ShowStats = args.Any(a => a.Equals(GoalStatsFlag, StringComparison.OrdinalIgnoreCase));

                if (result.ShowProgress || result.ShowStats)
                {
                    result.IsPresent = true;
                }

                return result;
            });
        }

        private static int? ParseWordCount(string arg)
        {
            var parts = arg.Split('=');
            if (parts.Length == 2 && int.TryParse(parts[1], out var count))
            {
                return count;
            }
            return null;
        }
    }
}
