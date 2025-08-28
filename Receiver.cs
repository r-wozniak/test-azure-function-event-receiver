using System;
using System.Text;
using System.Web;
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
        var now = DateTime.UtcNow;
        _logger.LogInformation($"Event counts: {events.Count()}");
        if (events.Count() > 0)
        {
            var firstEventJSon = Encoding.UTF8.GetString(events[0].Data);
            _logger.LogInformation($"First event in batch: {firstEventJSon}");

            var delay = GetDelay(now, firstEventJSon);
            _logger.LogInformation($"Processed: {now}, Delay (ms): {delay.Milliseconds}");

            //    "Id":21,"SendTime":"28/08/2025 09:12:41","M
        }
/*        foreach (EventData @event in events)
        {
            var data1= Encoding.UTF8.GetString(@event.Data);
            _logger.LogInformation($"First event in batch: {data1} Time: {DateTime.Now}");
        }*/
    }

    private TimeSpan GetDelay(DateTime now, string jsonRecord)
    {    
/*        int idIndx = jsonRecord.IndexOf("\"Id\":") + 5;
        int idEndIndx = jsonRecord.IndexOf(",", idIndx);
        string strId = jsonRecord.Substring(idIndx, idEndIndx - idIndx);

        int id = int.Parse(strId);*/

        int senddateIndx = jsonRecord.IndexOf("\"SendTime\":\"") + 12;
        _logger.LogInformation($"si: {senddateIndx}");
        int senddateEndIndx = jsonRecord.IndexOf("\",", senddateIndx);
        _logger.LogInformation($"sie: {senddateEndIndx}");
        string strSendDate = jsonRecord.Substring(senddateIndx, senddateEndIndx - senddateIndx);
        _logger.LogInformation($"date: {strSendDate}");
        DateTime sendDate = DateTime.Parse(strSendDate);
        _logger.LogInformation($"senddate: {sendDate}");
        return now - sendDate;
    }
}