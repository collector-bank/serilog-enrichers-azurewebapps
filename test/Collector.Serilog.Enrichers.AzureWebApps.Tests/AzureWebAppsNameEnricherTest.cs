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
            Environment.SetEnvironmentVariable("WEBSITE_SITE_NAME", null);
            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new AzureWebAppsNameEnricher("namename"))
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.IsTrue(logEvent.Properties.ContainsKey("AzureWebAppsName"));
            Assert.AreEqual("namename", ((ScalarValue)logEvent.Properties["AzureWebAppsName"]).Value);
        }

        [Test]
        public void When_not_using_a_default_name_local_is_set()
        {
            Environment.SetEnvironmentVariable("WEBSITE_SITE_NAME", null);
            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With<AzureWebAppsNameEnricher>()
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.IsTrue(logEvent.Properties.ContainsKey("AzureWebAppsName"));
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

    public class AzureWebAppsNameHelperTest
    {
        [Test]
        public void When_sitename_and_hostname_are_equal_doesnt_log_slotname()
        {
            Environment.SetEnvironmentVariable("WEBSITE_SITE_NAME", "AzureWebAppName");
            Environment.SetEnvironmentVariable("WEBSITE_HOSTNAME", "AzureWebAppName.azurewebsites.net");

            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new AzureWebAppsNameEnricher())
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.IsFalse(logEvent.Properties.ContainsKey("AzureWebAppsSlotName"));

        }

        [Test]
        public void When_sitename_and_hostname_differs_logs_slotname()
        {
            Environment.SetEnvironmentVariable("WEBSITE_SITE_NAME", "AzureWebAppName");
            Environment.SetEnvironmentVariable("WEBSITE_HOSTNAME", "AzureWebAppName-staging.azurewebsites.net");

            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new AzureWebAppsNameEnricher())
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.IsTrue(logEvent.Properties.ContainsKey("AzureWebAppsSlotName"));
            Assert.AreEqual("staging", ((ScalarValue)logEvent.Properties["AzureWebAppsSlotName"]).Value);

        }

        [Test]
        public void When_sitename_is_null_dont_log_slotname()
        {
            Environment.SetEnvironmentVariable("WEBSITE_SITE_NAME", null);
            Environment.SetEnvironmentVariable("WEBSITE_HOSTNAME", "AzureWebAppName-staging.azurewebsites.net");

            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new AzureWebAppsNameEnricher())
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.IsTrue(logEvent.Properties.ContainsKey("AzureWebAppsName"));
            Assert.AreEqual("LOCAL", ((ScalarValue)logEvent.Properties["AzureWebAppsName"]).Value);
            Assert.IsFalse(logEvent.Properties.ContainsKey("AzureWebAppsSlotName"));

        }
    }
}
