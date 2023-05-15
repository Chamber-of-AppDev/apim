using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class JobProcessor
    {
        private readonly ILogger _logger;

        public JobProcessor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JobProcessor>();
        }

        [Function("JobProcessor")]
        public void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] string myQueueItem)
        {
            Random rand = new Random();
            int sleepTimeInMilliSeconds = rand.Next(20000, 80000);

            _logger.LogInformation($"Job processing for {sleepTimeInMilliSeconds} milliseconds...");
            Thread.Sleep(sleepTimeInMilliSeconds);
        }
    }
}
