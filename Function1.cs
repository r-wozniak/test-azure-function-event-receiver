using System;
using System.Text;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Amqp;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TestAzureFunEventReceiver;
public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function(nameof(Function1))]
    public void Run([EventHubTrigger("roberteventhub", Connection = "EventHubConnectionString")] EventData[] events)
    {
        _logger.LogInformation($"Event Body: {events.Count()}");
        foreach (EventData @event in events)
        {
            var data1= Encoding.UTF8.GetString(@event.Data);
            var data2 = @event.Body.ToString();
            _logger.LogInformation($"Event Body1: {data1}");
            _logger.LogInformation($"Event Body2: {data2}");
            _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
        }
        _logger.LogInformation($"Time: {DateTime.Now}");
    }
}