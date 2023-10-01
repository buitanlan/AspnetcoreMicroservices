using Basket.API.Extensions;
using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information($"Starting {builder.Environment.ApplicationName} API up");
try
{
    // Add services to the container.
    builder.Host.UseSerilog(Serilogger.Configuration);
    builder.Configuration.AddAppConfigurations();
    builder.Services.ConfigureService();
    builder.Services.Configure<RouteOptions>(o => o.LowercaseUrls = true);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    app.Run();
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;
    Log.Fatal(ex, $"Unhandled exception {ex.Message}");
}
finally
{
    Log.Information("Shut down Product Api complete");
    Log.CloseAndFlush();
}
