using System;
using NUnit.Framework;
using Serilog;
using Serilog.Enrichers.AzureWebApps;
using Serilog.Events;

namespace Collector.Serilog.Enrichers.AzureWebApps.Tests
{
    public class AzureWebJobsTypeEnricherTest
    {
        [Test]
        public void Type_is_taken_from_environment_variables()
        {
            Environment.SetEnvironmentVariable("WEBJOBS_TYPE", "Continious");

            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new AzureWebJobsTypeEnricher())
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("Continious", ((ScalarValue)logEvent.Properties["AzureWebJobsType"]).Value);
        }

        [Test]
        public void Type_is_no_webjob_when_environment_variable_is_not_set()
        {
            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With<AzureWebJobsTypeEnricher>()
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("NO_WEBJOB", ((ScalarValue)logEvent.Properties["AzureWebJobsType"]).Value);

        }
        
    }
}
