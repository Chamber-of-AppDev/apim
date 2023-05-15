using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class SayHello
    {
        private readonly ILogger _logger;

        public SayHello(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SayHello>();
        }

        [Function("SayHello")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            Random rand = new Random();
            int sleepTimeInMilliSeconds = rand.Next(2000, 8000);

            _logger.LogInformation($"Sleeping for {sleepTimeInMilliSeconds} milliseconds...");
            Thread.Sleep(sleepTimeInMilliSeconds);

            response.WriteString($"Slept for {sleepTimeInMilliSeconds} milliseconds!");

            return response;
        }
    }
}
