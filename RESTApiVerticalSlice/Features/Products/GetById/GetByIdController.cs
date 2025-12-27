using MediatR;

using Microsoft.AspNetCore.Mvc;

using RESTApiVerticalSlice.Common.Logging;

namespace RESTApiVerticalSlice.Features.Products.GetById;

[ApiController]
[Route("api/products")]
public class GetByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [Log("GetProductById", LogLevel.Information)]
    public async Task<IActionResult> Handle(Guid id)
    {
        var item = await _mediator.Send(new GetProductByIdQuery(id));
        return item is not null ? Ok(item) : NotFound();
    }
}
