using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers.AzureWebApps
{
    public class AzureWebJobsTypeEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("AzureWebJobsType", Environment.GetEnvironmentVariable("WEBJOBS_TYPE") ?? "NO_WEBJOB"));
        }
    }
}
