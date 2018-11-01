using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers.AzureWebApps
{
    public class AzureWebAppsNameEnricher : ILogEventEnricher
    {
        private readonly string _defaultAppName;


        public AzureWebAppsNameEnricher() : this("LOCAL")
        {
        }

        public AzureWebAppsNameEnricher(string defaultAppName)
        {
            _defaultAppName = defaultAppName;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("AzureWebAppsName", AzureWebAppsNameHelper.GetWebAppsName(_defaultAppName)));
            var azureSlotName = AzureWebAppsNameHelper.GetSlotName();
            if (!string.IsNullOrWhiteSpace(azureSlotName))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("AzureWebAppsSlotName", azureSlotName));
            }
        }
    }
}
