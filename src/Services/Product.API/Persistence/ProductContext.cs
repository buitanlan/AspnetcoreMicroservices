using Microsoft.EntityFrameworkCore;
using Product.API.Entities;

namespace Product.API.Persistence;

public class ProductContext: DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
        
    }
    
    public DbSet<CatalogProduct> Products { get; set; }
}