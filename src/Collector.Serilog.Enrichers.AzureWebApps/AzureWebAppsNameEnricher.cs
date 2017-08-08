using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers.AzureWebApps
{
    public class AzureWebAppsNameEnricher : ILogEventEnricher
    {
        private readonly string _defaultAppName;


        public AzureWebAppsNameEnricher()
        {
            _defaultAppName = "LOCAL";
        }

        public AzureWebAppsNameEnricher(string defaultAppName)
        {
            _defaultAppName = defaultAppName;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("AzureWebAppsName", Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") ?? _defaultAppName));
        }
    }
}
