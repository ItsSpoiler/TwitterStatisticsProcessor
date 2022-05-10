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

        private readonly ITweetProcessorService _twitterProcessorService;
        private readonly ILogger<TwitterStatisticsController> _logger;

        public TwitterStatisticsController(ITweetProcessorService tweetProcessorService,ILogger<TwitterStatisticsController> logger)
        {
            _twitterProcessorService = tweetProcessorService;
            _logger = logger;
        }

        [HttpGet]
        public TwitterStatistics Get()
        {
            return _twitterProcessorService.GetStatistics();
        }
    }
}
