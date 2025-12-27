using MediatR;

using Microsoft.AspNetCore.Mvc;

using RESTApiVerticalSlice.Common.Logging;

namespace RESTApiVerticalSlice.Features.Products.Delete;

[ApiController]
[Route("api/products")]
public class DeleteController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    [Log("DeleteProduct", LogLevel.Error)]
    public async Task<IActionResult> Handle(Guid id)
    {
        var removed = await _mediator.Send(new DeleteProductCommand(id));
        return removed ? NoContent() : NotFound();
    }
}
