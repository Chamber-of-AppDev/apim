using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using System.Text;

namespace Company.Function
{
    public class PopulateQueueMessages
    {
        private readonly ILogger _logger;
        private static readonly HttpClient httpClient = new HttpClient();

        public PopulateQueueMessages(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PopulateQueueMessages>();
        }

        [Function("PopulateQueueMessages")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, FunctionContext context)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            string queueName = "myqueue-items";

            var queueClient = new QueueClient(connectionString, queueName);
            queueClient.CreateIfNotExists();

            // Generate and enqueue 1000 random messages
            for (int i = 0; i < 1000; i++)
            {
                string message = $"Message_{Guid.NewGuid()}";
                queueClient.SendMessage(Convert.ToBase64String(Encoding.UTF8.GetBytes(message)));
            }

            // Get function URL dynamically
            int port = req.Url.IsDefaultPort ? -1 : req.Url.Port;
            string accessKey = Environment.GetEnvironmentVariable("PopulateQueueMessages-FunctionKey");
            string functionUrl = $"{req.Url.Scheme}://{req.Url.Host}{(port > 0 ? $":{port}" : "")}{req.Url.AbsolutePath}?code={accessKey}";


            // Call the same function in a fire and forget fashion
            _ = httpClient.GetAsync(functionUrl);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString("Queue Population started!");
            return response;
        }
    }
}
