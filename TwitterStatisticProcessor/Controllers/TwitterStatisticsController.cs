using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TwitterStatisticProcessor.Models;
using TwitterStatisticProcessor.Services;

namespace TwitterStatisticProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitterStatisticsController : ControllerBase
    {

        private readonly ILogger<TwitterStatisticsController> _logger;
        private readonly ITweetRepository _tweetRepo;

        public TwitterStatisticsController(ILogger<TwitterStatisticsController> logger, ITweetRepository tr)
        {
            _tweetRepo = tr;
            _logger = logger;
        }

        [HttpGet]
        public TwitterStatistics Get()
        {
            return new TwitterStatistics(_tweetRepo.GetTweetCount(), _tweetRepo.GetAverageTweetCount());
        }
    }
}
