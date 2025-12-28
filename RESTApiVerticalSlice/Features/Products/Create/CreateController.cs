using MediatR;

using Microsoft.AspNetCore.Mvc;

using RESTApiVerticalSlice.Common.Logging;

namespace RESTApiVerticalSlice.Features.Products.Create;

[ApiController]
[Route("api/products")]
public class CreateController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Log("CreateProduct", LogLevel.Warning)]
    public async Task<IActionResult> Handle([FromBody] CreateProductCommand command)
    {
        var created = await _mediator.Send(command);
        return Created($"/api/products/{created.Id}", created);
    }
}
