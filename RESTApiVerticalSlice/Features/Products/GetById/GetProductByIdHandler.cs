using MediatR;

using RESTApiVerticalSlice.Storage;

namespace RESTApiVerticalSlice.Features.Products.GetById;

public sealed class GetProductByIdQuery : IRequest<GetByIdProductResponseDto?>
{
    public Guid Id { get; }
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetByIdProductResponseDto?>
{
    private readonly InMemoryProductStorage _storage;
    public GetProductByIdHandler(InMemoryProductStorage storage)
    {
        _storage = storage;
    }

    public async Task<GetByIdProductResponseDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _storage.GetByIdAsync(request.Id);
        return product is null ? null : new GetByIdProductResponseDto(product.Id, product.Name, product.Price);
    }
}
