using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


    
Log.Information("Starting Product API up");
try
{
    // Add services to the container.
    builder.Host.UseSerilog(Serilogger.Configuration); 
    builder.Configuration.AddAppConfigurations();
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    
    app.UseInfrastructure();

    app.MigrateDatabase<ProductContext>((context, _) =>
        {
            ProductContextSeed.SeedProductAsync(context).Wait();
        })
        .Run();

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

