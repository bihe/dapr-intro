using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_subscriber.Controllers
{
    public sealed class Subscription 
    {
        [JsonPropertyName("pubsubname")]
        public string PubSubName { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("route")]
        public string Route { get; set; }
    }

    public sealed class Result
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }

    public sealed class Message
    {
        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public sealed class Data
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    [ApiController]
    [Produces("application/json")]
    public class PubSubController : ControllerBase
    {
        private readonly ILogger<PubSubController> _logger;
        const string PubSubName = "pubsub";

        public PubSubController(ILogger<PubSubController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/dapr/subscribe")]
        public List<Subscription> GetSubscription()
        {
            return new List<Subscription>{
                new Subscription{
                    PubSubName = PubSubName,
                    Topic = "ALL",
                    Route = "receive_all"
                },
                new Subscription{
                   PubSubName = PubSubName,
                    Topic = "Topic2",
                    Route = "receive_c"
                }
            };
        }

        [HttpPost("/receive_all")]
        public IActionResult GetSubscriptionResultA([FromBody]Message msg) 
        {
            _logger.LogInformation($"📜 message '{msg.Data.Message}' via '/receive_all' for '{msg.Topic}'");
            return Ok(new Result {
                Success = true
            });
        }

        [HttpPost("/receive_c")]
        public IActionResult GetSubscriptionResultC([FromBody]Message msg) 
        {
            _logger.LogInformation($"📜 message '{msg.Data.Message}' via '/receive_c' for '{msg.Topic}'");
            return Ok(new Result {
                Success = true
            });
        }
    }
}
