using Customer.API.Services.Interfaces;

namespace Customer.API.Controllers;

public static class CustomersController
{
    public static void MapCustomerApi(this WebApplication app)
    {
        app.MapGet("/api/customers", async (ICustomerService customerService) => await customerService.GetCustomerAsync());
        app.MapGet("/api/customers/{username}",
            async (ICustomerService customerService, string username) =>
                await customerService.GetCustomerByUsernameAsync(username));

    }
}
