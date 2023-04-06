using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories;

public class ProductRepository : RepositoryBaseAsync<CatalogProduct, int, ProductContext>, IProductRepository
{
    public ProductRepository(ProductContext context, IUnitOfWork<ProductContext> unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<IEnumerable<CatalogProduct>> GetProducts()
    {
        return await FindAll().ToListAsync();
    }

    public Task<CatalogProduct> GetProduct(int id)
    {
        return GetByIdAsync(id);
    }

    public Task<CatalogProduct> GetProductByNo(string productNo)
    {
        return FindByCondition(x => x.No.Equals(productNo)).SingleOrDefaultAsync();
    }

    public async Task CreateProduct(CatalogProduct product)
    {
        await CreateAsync(product);
    }

    public async Task UpdateProduct(CatalogProduct product)
    {
        await UpdateAsync(product);
    }

    public async Task DeleteProduct(int id)
    {
        var product = await GetProduct(id);
        if(product != null)
            await DeleteAsync(product);
    }
}