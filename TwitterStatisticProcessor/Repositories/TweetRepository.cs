using System;
using System.Threading.Tasks;
using TwitterStatisticProcessor.Services;

namespace TwitterStatisticProcessor.Models
{
    public class TweetRepository : ITweetRepository
    {
        private int tweetCount = 0;
        private DateTime startTime;

        public float GetAverageTweetCount()
        {
            TimeSpan timeElapsed = DateTime.Now - startTime;
            int minutes = timeElapsed.Minutes;
            if(minutes == 0) minutes = 1;
            return tweetCount / minutes;
        }

        public int GetTweetCount()
        {
            return tweetCount;
        }

        public void IncrementTweetCount()
        {
            tweetCount++;
        }

        public void SetStartTime(DateTime appStartTime)
        {
            startTime = appStartTime;
        }
    }
    public interface ITweetRepository
    {
        void IncrementTweetCount();
        int GetTweetCount();
        float GetAverageTweetCount();
        void SetStartTime(DateTime appStartTime);
    }

}