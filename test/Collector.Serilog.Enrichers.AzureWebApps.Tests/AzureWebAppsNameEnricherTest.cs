using System;
using NUnit.Framework;
using Serilog;
using Serilog.Enrichers.AzureWebApps;
using Serilog.Events;

namespace Collector.Serilog.Enrichers.AzureWebApps.Tests
{
    public class AzureWebAppsNameEnricherTest
    {
        [Test]
        public void When_using_a_default_name_it_is_set()
        {
            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new AzureWebAppsNameEnricher("namename"))
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("namename", ((ScalarValue)logEvent.Properties["AzureWebAppsName"]).Value);
        }

        [Test]
        public void When_not_using_a_default_name_local_is_set()
        {
            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With<AzureWebAppsNameEnricher>()
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("LOCAL", ((ScalarValue)logEvent.Properties["AzureWebAppsName"]).Value);

        }

        [Test]
        public void When_environtment_variable_is_set_it_is_also_used()
        {
            Environment.SetEnvironmentVariable("WEBSITE_SITE_NAME","AzureWebAppName");

            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new AzureWebAppsNameEnricher("namename"))
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("AzureWebAppName", ((ScalarValue)logEvent.Properties["AzureWebAppsName"]).Value);

        }
    }
}
