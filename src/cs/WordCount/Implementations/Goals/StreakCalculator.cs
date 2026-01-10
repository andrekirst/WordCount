using System;
using System.Linq;
using WordCount.Interfaces.Goals;
using WordCount.Models.Goals;

namespace WordCount.Implementations.Goals
{
    public class StreakCalculator : IStreakCalculator
    {
        public void UpdateStreak(GoalData data)
        {
            var today = DateTime.UtcNow.Date;
            var yesterday = today.AddDays(-1);

            var hasActivityToday = data.ProgressEntries.Any(p => p.Date.Date == today);
            var lastActivity = data.ProgressEntries
                .OrderByDescending(p => p.Date)
                .FirstOrDefault();

            if (lastActivity == null)
            {
                data.CurrentStreak = new Streak();
                return;
            }

            var lastActivityDate = lastActivity.Date.Date;
            data.CurrentStreak.LastActivityDate = lastActivityDate;

            if (hasActivityToday)
            {
                if (data.CurrentStreak.StreakStartDate == null ||
                    lastActivityDate == today ||
                    (lastActivityDate == yesterday && data.CurrentStreak.CurrentStreak > 0))
                {
                    if (data.CurrentStreak.StreakStartDate == null)
                    {
                        data.CurrentStreak.StreakStartDate = today;
                        data.CurrentStreak.CurrentStreak = 1;
                    }
                    else if (lastActivityDate != today)
                    {
                        data.CurrentStreak.CurrentStreak++;
                    }
                }
                else
                {
                    data.CurrentStreak.StreakStartDate = today;
                    data.CurrentStreak.CurrentStreak = 1;
                }
            }
            else if (lastActivityDate < yesterday)
            {
                data.CurrentStreak.CurrentStreak = 0;
                data.CurrentStreak.StreakStartDate = null;
            }

            if (data.CurrentStreak.CurrentStreak > data.CurrentStreak.LongestStreak)
            {
                data.CurrentStreak.LongestStreak = data.CurrentStreak.CurrentStreak;
            }
        }

        public Streak GetCurrentStreak(GoalData data)
        {
            UpdateStreak(data);
            return data.CurrentStreak;
        }

        public bool IsStreakActive(GoalData data)
        {
            var today = DateTime.UtcNow.Date;
            var yesterday = today.AddDays(-1);

            var lastActivity = data.CurrentStreak.LastActivityDate;

            if (lastActivity == null)
            {
                return false;
            }

            return lastActivity.Value.Date == today || lastActivity.Value.Date == yesterday;
        }
    }
}
