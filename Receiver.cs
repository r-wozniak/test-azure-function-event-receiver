using System;
using System.Text;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Amqp;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TestAzureFunEventReceiver;
public class Receiver
{
    private readonly ILogger<Receiver> _logger;

    public Receiver(ILogger<Receiver> logger)
    {
        _logger = logger;
    }

    [Function(nameof(Receiver))]
    public void Run([EventHubTrigger("roberteventhub", Connection = "EventHubConnectionString", ConsumerGroup = "azure")] EventData[] events)
    {
        _logger.LogInformation($"Event counts: {events.Count()}");
        if (events.Count() > 0)
        {
            var data = Encoding.UTF8.GetString(events[0].Data);
            _logger.LogInformation($"First event in batch: {data}");
            _logger.LogInformation($"Processed: {DateTime.UtcNow}");
            _logger.LogInformation("");
        }
/*        foreach (EventData @event in events)
        {
            var data1= Encoding.UTF8.GetString(@event.Data);
            _logger.LogInformation($"First event in batch: {data1} Time: {DateTime.Now}");
        }*/
    }
}