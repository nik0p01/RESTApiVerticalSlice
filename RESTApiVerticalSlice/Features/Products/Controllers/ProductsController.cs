using MediatR;
using Microsoft.AspNetCore.Mvc;
using RESTApiVerticalSlice.Common.Logging;
using RESTApiVerticalSlice.Features.Products.Models;
using RESTApiVerticalSlice.Features.Products.Services.Handlers;

namespace RESTApiVerticalSlice.Features.Products.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Log("GetProducts", LogLevel.Information)]
    public async Task<IActionResult> Get()
    {
        var items = await _mediator.Send(new GetAllProductsQuery());
        return Ok(items);
    }

    [HttpGet("{id}")]
    [Log("GetProductById", LogLevel.Information)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _mediator.Send(new GetProductByIdQuery(id));
        return item is not null ? Ok(item) : NotFound();
    }

    [HttpPost]
    [Log("CreateProduct", LogLevel.Warning)]
    public async Task<IActionResult> Create([FromBody] ProductDto dto)
    {
        var created = await _mediator.Send(new CreateProductCommand(dto));
        return Created($"/api/products/{created.Id}", created);
    }

    [HttpPut("{id}")]
    [Log("UpdateProduct", LogLevel.Warning)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
    {
        var updated = await _mediator.Send(new UpdateProductCommand(id, dto));
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    [Log("DeleteProduct", LogLevel.Error)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var removed = await _mediator.Send(new DeleteProductCommand(id));
        return removed ? NoContent() : NotFound();
    }
}
