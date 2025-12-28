using MediatR;

using RESTApiVerticalSlice.Storage;
using RESTApiVerticalSlice.Storage.Domain;

namespace RESTApiVerticalSlice.Features.Products.Create;

public sealed class CreateProductCommand : IRequest<CreateProductResponseDto>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponseDto>
{
    private readonly InMemoryProductStorage _storage;

    public CreateProductHandler(InMemoryProductStorage storage)
    {
        _storage = storage;
    }

    public async Task<CreateProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(Guid.NewGuid(), request.Name, request.Price);
        var created = await _storage.CreateAsync(product);
        return new CreateProductResponseDto(created.Id, created.Name, created.Price);
    }
}
