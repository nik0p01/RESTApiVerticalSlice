using MediatR;

using Microsoft.AspNetCore.Mvc;

using RESTApiVerticalSlice.Common.Logging;

namespace RESTApiVerticalSlice.Features.Products.GetAll;

[ApiController]
[Route("api/products")]
public class GetAllController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Log("GetProducts", LogLevel.Information)]
    public async Task<IActionResult> Handle()
    {
        var items = await _mediator.Send(new GetAllProductsQuery());
        return Ok(items);
    }
}
