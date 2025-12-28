using MediatR;

using RESTApiVerticalSlice.Storage;
using RESTApiVerticalSlice.Storage.Domain;

namespace RESTApiVerticalSlice.Features.Products.Update;

public sealed class UpdateProductCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }

    public UpdateProductCommand() { }

    public UpdateProductCommand(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly InMemoryProductStorage _storage;
    public UpdateProductHandler(InMemoryProductStorage storage)
    {
        _storage = storage;
    }

    public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Id, request.Name, request.Price);
        return _storage.UpdateAsync(product);
    }
}
