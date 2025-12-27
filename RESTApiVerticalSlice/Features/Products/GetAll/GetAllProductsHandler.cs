using MediatR;

using RESTApiVerticalSlice.Storage;

namespace RESTApiVerticalSlice.Features.Products.GetAll;

public sealed class GetAllProductsQuery : IRequest<IEnumerable<GetAllProductResponseDto>> { }

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductResponseDto>>
{
    private readonly InMemoryProductStorage _storage;

    public GetAllProductsHandler(InMemoryProductStorage storage)
    {
        _storage = storage;
    }

    public async Task<IEnumerable<GetAllProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _storage.GetAllAsync();
        return products.Select(p => new GetAllProductResponseDto(p.Id, p.Name, p.Price));
    }
}
