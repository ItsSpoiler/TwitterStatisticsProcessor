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
        Task ProcessTweets();
    }
    public class TweetProcessorService : BackgroundService, ITweetProcessorService
    {
        private DateTime ApplicationStartTime = DateTime.Now;
        private readonly ITweetRepository tr;

        public TweetProcessorService(ITweetRepository tr)
        {
            this.tr = tr;
        }

        public async Task ProcessTweets()
        {
            string AccessToken = "AAAAAAAAAAAAAAAAAAAAAAJocQEAAAAASCvTeykliHE1maTYL%2BUHsa4lC4I%3DLavVdHUR8U7ylzMSR9GOwSjndGDt5EorfIH7S2Og7K9eYScWOH";
            HttpClient client = new HttpClient();
            Uri twitterUrl;
            Uri.TryCreate("https://api.twitter.com/", new UriCreationOptions(), out twitterUrl);
            client.BaseAddress = twitterUrl;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            var response = await client.GetStreamAsync("/2/tweets/sample/stream");
            var sr = new StreamReader(response);

            tr.SetStartTime(ApplicationStartTime);

            var str = await sr.ReadLineAsync();
            while (str != null) {
                tr.IncrementTweetCount();
                Console.WriteLine(str);
                str = await sr.ReadLineAsync();
            }



            // request.Timeout = Timeout.Infinite;
            // HttpWebResponse response = client.GetResponseAsync();
            // StreamReader reader = new StreamReader(response.GetResponseStream());

            // string str = reader.ReadLine();

            // while (str != null)
            // {
            //     numberOfTweets++;
            //     Console.WriteLine(str);
            //     str = reader.ReadLine();

            // }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) {
                await ProcessTweets();
            } 
        }
    }

}