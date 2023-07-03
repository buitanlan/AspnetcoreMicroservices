using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Services;

public class CustomerService(ICustomerRepository repository) : ICustomerService
{
    public async Task<IResult> GetCustomerByUsernameAsync(string username)
        => Results.Ok(await repository.GetCustomerByUserNameAsync(username));

    public async Task<IResult> GetCustomerAsync()
        => Results.Ok(await repository.GetCustomerAsync());
}
