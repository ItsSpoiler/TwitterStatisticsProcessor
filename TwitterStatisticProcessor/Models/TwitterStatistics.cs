using System;

namespace TwitterStatisticProcessor.Models
{
    public class TwitterStatistics
    {
        public TwitterStatistics(int totalNumberOfTweets, float averageTweetsPerMinute)
        {
            TotalNumberOfTweets = totalNumberOfTweets;
            AverageTweetsPerMinute = averageTweetsPerMinute;
        }

        public int TotalNumberOfTweets { get; set; }
        public float AverageTweetsPerMinute { get; set; }
    }
}
