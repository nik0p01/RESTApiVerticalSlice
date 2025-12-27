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
    public async Task<IActionResult> Handle(Guid id, [FromBody] UpdateProductRequestDto dto)
    {
        var updated = await _mediator.Send(new UpdateProductCommand(id, dto));
        return updated ? NoContent() : NotFound();
    }
}
