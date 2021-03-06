using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class TimerTriggerCSharp1
    {
        [FunctionName("TimerTriggerCSharp1")]
        public static void Run([TimerTrigger("*/20 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            Console.WriteLine("Its working.. yey");
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
