using Basket.API.Entities;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

namespace Basket.API.Repositories;

public class BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService) : IBasketRepository
{
    public async Task<Cart?> GetBasketByUserName(string userName)
    {
        var basket = await redisCacheService.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket) ? null : serializeService.Deserialize<Cart>(basket);
    }

    public async Task<Cart?> UpdateBasket(Cart cart, DistributedCacheEntryOptions? options)
    {
        Log.Information($"BEGIN: Update Basket for{cart.Username}");
        options ??= new DistributedCacheEntryOptions();
        await redisCacheService.SetStringAsync(cart.Username, serializeService.Serialize(cart), options);
        Log.Information($"END: Update Basket for{cart.Username}");
        return await GetBasketByUserName(cart.Username);
    }

    public async Task<bool> DeleteBasketFromUserName(string username)
    {
        try
        {
            Log.Information($"BEGIN: Delete Basket for {username}");
            await redisCacheService.RemoveAsync(username);
            Log.Information($"END: Delete Basket for {username}");
            return true;
        }
        catch (Exception e)
        {
            Log.Fatal($"DeleteBasketFromUser: {e.Message}");
            throw;
        }
    }
}
