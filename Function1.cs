using System;
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
    public void Run([EventHubTrigger("roberteventhub1", Connection = "Endpoint=sb://roberteventhub.servicebus.windows.net/;SharedAccessKeyName=TestPolicy;SharedAccessKey=Dnco7aZscO9EwITfdVjwQ3WpHYm6wChHy+AEhPUWwnE=;EntityPath=roberteventhub1")] EventData[] events)
    {
        _logger.LogInformation($"Event Body: {events.Count()}");
        foreach (EventData @event in events)
        {
            _logger.LogInformation("Event Body: {body}", @event.Body);
            _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
        }
        _logger.LogInformation($"Time: {DateTime.Now}");
    }
}