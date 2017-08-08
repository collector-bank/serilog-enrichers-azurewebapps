# Serilog.Enrichers.AzureWebApps

[![Build status](https://ci.appveyor.com/api/projects/status/np642cuxtlrfrnm6/branch/master?svg=true)](https://ci.appveyor.com/project/iremmats/serilog-enrichers-azurewebapps/branch/master)

Enrichers that add properties from Azure Web Apps environment variables.
 
To use the enrichers, first install the NuGet package:

```powershell
Install-Package Collector.Serilog.Enrichers.AzureWebApps
```

Then, apply the enrichers to your `LoggerConfiguration`:

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.With<AzureWebAppsNameEnricher>()
    .Enrich.With<AzureWebJobsNameEnricher>()
    .Enrich.With<AzureWebJobsTypeEnricher>()
    // ...other configuration...
    .CreateLogger();
```

### Included enrichers

The package includes:

 * `AzureWebAppsNameEnricher` - adds the name of the Azure WebApp the application runs within
 * `AzureWebJobsNameEnricher` - adds the name of the Azure WebJob (if the application is a WebJob)
 * `AzureWebJobsTypeEnricher` - adds the type name of the Azure WebJob (continuous or triggered)
