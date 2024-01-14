using Customer.API.Services.Interfaces;

namespace Customer.API.Controllers;

public static class CustomersController
{
    public static void MapCustomerApi(this WebApplication app)
    {
        app.MapGet("/api/customers", (ICustomerService customerService) => customerService.GetCustomerAsync());
        app.MapGet("/api/customers/{username}", (ICustomerService customerService, string username) =>
                customerService.GetCustomerByUsernameAsync(username));
    }
}
