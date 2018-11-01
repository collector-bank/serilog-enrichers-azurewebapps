using System;
using System.Linq;

namespace Serilog.Enrichers.AzureWebApps
{
    public static class AzureWebAppsNameHelper
    {
        private const string WebsiteSiteNameEnv = "WEBSITE_SITE_NAME";
        private const string WebsiteHostNameEnv = "WEBSITE_HOSTNAME";

        public static string GetWebAppsName(string defaultWebAppsName)
        {
            return Environment.GetEnvironmentVariable(WebsiteSiteNameEnv) ?? defaultWebAppsName;
        }

        public static string GetSlotName()
        {
            var siteName = Environment.GetEnvironmentVariable(WebsiteSiteNameEnv)?.ToLowerInvariant();
            var hostname = Environment.GetEnvironmentVariable(WebsiteHostNameEnv)?.ToLowerInvariant().StripDomain();
            if (siteName == null || hostname == null)
                return null;
            if (string.Equals(siteName, hostname, StringComparison.OrdinalIgnoreCase))
                return null;
            return hostname.Replace(siteName, string.Empty).Trim('-');
        }

        private static string StripDomain(this string hostname)
        {
            var index = hostname.IndexOf('.');
            if (index <= 0) return null;
            return hostname.Substring(0, index);
        }
    }
}
