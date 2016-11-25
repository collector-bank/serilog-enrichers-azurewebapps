# serilog-enrichers-azurewebapps

[![Build status](https://ci.appveyor.com/api/projects/status/np642cuxtlrfrnm6/branch/master?svg=true)](https://ci.appveyor.com/project/iremmats/serilog-enrichers-azurewebapps/branch/master)

Enrichers that adds properties from Azure Web Apps environment variables.
 
To use the enrichers, first install the NuGet package:

```powershell
Install-Package Serilog.Enrichers.AzureWebApps
```

Then, apply the enrichers to you `LoggerConfiguration`:

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.With(new AzureWebAppsNameEnricher)
    .Enrich.With(new AzureWebJobsNameEnricher)
    .Enrich.With(new AzureWebJobsTypeEnricher)
    // ...other configuration...
    .CreateLogger();
```

### Included enrichers

The package includes:

 * `AzureWebAppsNameEnricher()` - adds the name of the Azure WebApp the application runs within
 * `AzureWebJobsNameEnricher()` - adds the name of the Azure WebJob (if the application is a webjob)
 * `AzureWebJobsTypeEnricher()` - adds the typename of the Azure WebJob (continious or triggered)
