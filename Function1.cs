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
        if (events.Count() > 0)
        {
            var data = Encoding.UTF8.GetString(events[0].Data);
            _logger.LogInformation($"First event in batch: {data} Time: {DateTime.Now}");
        }
/*        foreach (EventData @event in events)
        {
            var data1= Encoding.UTF8.GetString(@event.Data);
            _logger.LogInformation($"First event in batch: {data1} Time: {DateTime.Now}");
        }*/
    }
}