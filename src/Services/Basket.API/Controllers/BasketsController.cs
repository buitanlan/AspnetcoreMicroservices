using System.ComponentModel.DataAnnotations;
using System.Net;
using Basket.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Controllers;

public class BasketsController(IBasketRepository basketRepository) : ControllerBase
{
    [HttpGet("{username}", Name = "GetBasket")]
    [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Cart>> GetBasketByUserName([Required] string usename)
    {
        var result = await basketRepository.GetBasketByUserName(usename);
        return Ok(result ?? new Cart());
    }

    [HttpPost(Name = "UpdateBasket")]
    [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Cart>> UpdateBasket([FromBody] Cart cart)
    {
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1))
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        var  result = await basketRepository.UpdateBasket(cart, options);
        return Ok(result);
    }

    [HttpDelete("{username", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteBasketFromUserName([Required] string usename)
    {
        var result = await basketRepository.DeleteBasketFromUserName(usename);
        return Ok(result);
    }
 }
