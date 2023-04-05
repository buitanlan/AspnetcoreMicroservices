using Common.Logging;
using Product.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


    
Log.Information("Starting Product API up");
try
{
    // Add services to the container.
    builder.Host.UseSerilog(Serilogger.Configuration); 
    builder.Configuration.AddAppConfigurations();
    builder.Services.AddInfrastructure();

    var app = builder.Build();
    
    app.UseInfrastructure();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Product Api complete");
    Log.CloseAndFlush();
}

