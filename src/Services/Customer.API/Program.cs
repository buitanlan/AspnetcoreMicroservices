using Common.Logging;
using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Host.UseSerilog(Serilogger.Configuration);
    
Log.Information("Starting Customer API up");
try
{
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddNpgsql<CustomerContext>(connectionString);

    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
        .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
        .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
        .AddScoped<ICustomerService, CustomerService>();

    var app = builder.Build();

    app.MapGet("/", () => "Welcome to Customer API!");
    app.MapGet("/api/customers", async (ICustomerService customerService) => await customerService.GetCustomerAsync());
    app.MapGet("/api/customers/{username}",
        async (ICustomerService customerService, string username) =>
            await customerService.GetCustomerByUsernameAsync(username));


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapDefaultControllerRoute();

    app.SeedCustomer();

    app.Run();
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if(type.Equals("StopTheHostException", StringComparison.Ordinal))
        throw;
    Log.Fatal(ex, "Unhanded exception");}
finally
{
    Log.Information("Shut down Customer Api complete");
    Log.CloseAndFlush();
}
