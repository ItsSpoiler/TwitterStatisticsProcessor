using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TwitterStatisticProcessor.Models;

namespace TwitterStatisticProcessor.Services
{
    public interface ITweetProcessorService
    {
        TwitterStatistics GetStatistics();

        Task ProcessTweets();
    }
    public class TweetProcessorService : BackgroundService, ITweetProcessorService
    {
        private int numberOfTweets = 0;
        private float averageTweetsPerMinute = 0;
        private DateTime ApplicationStartTime = DateTime.Now;

        public TwitterStatistics GetStatistics()
        {
            TimeSpan timeElapsed = DateTime.Now - ApplicationStartTime;
            averageTweetsPerMinute = numberOfTweets / timeElapsed.Minutes;
            return new TwitterStatistics(numberOfTweets, averageTweetsPerMinute);
        }

        public async Task ProcessTweets()
        {
            await Task.Run(() =>
            {
                string AccessToken = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitter.com/2/tweets/sample/stream");
                request.Headers.Add("Authorization", "Bearer " + AccessToken);
                request.Timeout = Timeout.Infinite;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string str = reader.ReadLine();

                while (str != null)
                {
                    numberOfTweets++;
                    Console.WriteLine(str);
                    str = reader.ReadLine();

                }
            });
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ProcessTweets();
        }
    }
}