using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers.AzureWebApps
{
    public class AzureWebJobsNameEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("AzureWebJobsName", Environment.GetEnvironmentVariable("WEBJOBS_NAME") ?? "NO_WEBJOB"));
        }
    }
}
