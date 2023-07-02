namespace Product.API. Extensions;

public static class ConfigureExtensions
{
    public static void AddAppConfigurations (this ConfigurationManager  configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        configuration.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true);
        configuration.AddJsonFile(path: $"appsettings.{environment}.json", optional: true, reloadOnChange: true);
        configuration.AddEnvironmentVariables();
    }
}