using MediatR;

using Microsoft.AspNetCore.Mvc;

using RESTApiVerticalSlice.Common.Logging;

namespace RESTApiVerticalSlice.Features.Products.Update;

[ApiController]
[Route("api/products")]
public class UpdateController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id}")]
    [Log("UpdateProduct", LogLevel.Warning)]
    public async Task<IActionResult> Handle(Guid id, [FromBody] UpdateProductCommand dto)
    {
        var command = new UpdateProductCommand(id, dto.Name, dto.Price);
        var updated = await _mediator.Send(command);
        return updated ? NoContent() : NotFound();
    }
}
