using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Entities;
using Product.API.Repositories.Interfaces;
using Shared.Dtos;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repository, IMapper mapper) : ControllerBase
{
    #region CRUD
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var products = await repository.GetProducts();
        var result = mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([Required] int id)
    {
        var product = await repository.GetProduct(id);
        if (product == null)
            return NotFound();

        var result = mapper.Map<ProductDto>(product);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
    {
        var product = mapper.Map<CatalogProduct>(productDto);
        await repository.CreateProduct(product);
        await repository.SaveChangesAsync();

        var result = mapper.Map<ProductDto>(product);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([Required] int id, [FromBody] UpdateProductDto productDto)
    {
        var product = await repository.GetProduct(id);
        if (product == null)
            return NotFound();

        var updateProduct = mapper.Map(productDto, product);
        await repository.UpdateProduct(updateProduct);
        await repository.SaveChangesAsync();

        var result = mapper.Map<ProductDto>(updateProduct);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([Required] int id)
    {
        var product = await repository.GetProduct(id);
        if (product == null)
            return NotFound();

        await repository.DeleteProduct(id);
        await repository.SaveChangesAsync();

        return NoContent();
    }
    #endregion


    #region Additional Resources
    [HttpGet("productNo")]
    public async Task<IActionResult> GetByNo([Required] string productNo)
    {
        var product = await repository.GetProductByNo(productNo);
        if (product == null)
            return NotFound();

        var result = mapper.Map<ProductDto>(product);
        return Ok(result);
    }
    #endregion
}
