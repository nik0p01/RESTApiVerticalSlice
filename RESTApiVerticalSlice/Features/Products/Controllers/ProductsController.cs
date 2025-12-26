using Microsoft.AspNetCore.Mvc;
using RESTApiVerticalSlice.Common.Logging;
using RESTApiVerticalSlice.Features.Products.Models;
using RESTApiVerticalSlice.Features.Products.Services;

namespace RESTApiVerticalSlice.Features.Products.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    [Log("GetProducts", LogLevel.Information)]
    public async Task<IActionResult> Get()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    [Log("GetProductById", LogLevel.Information)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is not null ? Ok(item) : NotFound();
    }

    [HttpPost]
    [Log("CreateProduct", LogLevel.Warning)]
    public async Task<IActionResult> Create([FromBody] ProductDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return Created($"/api/products/{created.Id}", created);
    }

    [HttpPut("{id}")]
    [Log("UpdateProduct", LogLevel.Warning)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    [Log("DeleteProduct", LogLevel.Error)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var removed = await _service.DeleteAsync(id);
        return removed ? NoContent() : NotFound();
    }
}
