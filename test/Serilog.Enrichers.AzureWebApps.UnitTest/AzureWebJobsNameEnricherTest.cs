
using System;
using NUnit.Framework;
using Serilog.Events;

namespace Serilog.Enrichers.AzureWebApps.UnitTest
{
    public class AzureWebJobsNameEnricherTest
    {
        [Test]
        public void Name_is_taken_from_environment_variables()
        {
            Environment.SetEnvironmentVariable("WEBJOBS_NAME", "MyWebjob");

            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new AzureWebJobsNameEnricher())
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("MyWebjob", ((ScalarValue)logEvent.Properties["AzureWebJobsName"]).Value);
        }

        [Test]
        public void Name_is_no_webjob_when_environment_variable_is_not_set()
        {
            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With<AzureWebJobsNameEnricher>()
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("NO_WEBJOB", ((ScalarValue)logEvent.Properties["AzureWebJobsName"]).Value);

        }
        
    }
}
