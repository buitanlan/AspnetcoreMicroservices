using Basket.API.Repositories;
using Contracts.Common.Interfaces;
using Infrastructure.Common;

namespace Basket.API.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection ConfigureService(this IServiceCollection services)
        => services
            .AddScoped<IBasketRepository, BasketRepository>()
            .AddTransient<ISerializeService, SerializeService>();
}
