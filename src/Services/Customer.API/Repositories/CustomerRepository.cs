using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories;

public class CustomerRepository(CustomerContext context) : RepositoryQueryBase<Entities.Customer, int, CustomerContext>(context), ICustomerRepository
{
    public async Task<Entities.Customer> GetCustomerByUserNameAsync(string username)
        => await FindByCondition(x => x.UserName.Equals(username)).SingleOrDefaultAsync();

    public async Task<IEnumerable<Entities.Customer>> GetCustomerAsync()
        => await FindAll().ToListAsync();
}
