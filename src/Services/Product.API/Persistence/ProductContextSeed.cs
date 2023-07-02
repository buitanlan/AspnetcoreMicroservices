using Product.API.Entities;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Product.API.Persistence;

public class ProductContextSeed
{
    public static async Task SeedProductAsync(ProductContext productContext)
    {
        if (!productContext.Products.Any())
        {
            productContext.AddRange(GetCatalogProduct());
            await productContext.SaveChangesAsync();
            Log.Information("Seeded data for ProductDB associated with context {DbContextName}", nameof(Product));
        }
    }

    private static IEnumerable<CatalogProduct> GetCatalogProduct()
    {
        return new List<CatalogProduct>
        {
            new()
            {
                No = "Latus",
                Name = "Esprit",
                Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                Price = (decimal)17789.23
            },
            new()
            {
                No = "Cadillac",
                Name = "CTS",
                Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                Price = (decimal)2332.23
            }
        };
    }
}
