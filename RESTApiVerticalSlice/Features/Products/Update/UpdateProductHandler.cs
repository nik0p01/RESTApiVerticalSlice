using MediatR;

using RESTApiVerticalSlice.Storage;
using RESTApiVerticalSlice.Storage.Models;

namespace RESTApiVerticalSlice.Features.Products.Update;

public sealed class UpdateProductCommand : IRequest<bool>
{
    public Guid Id { get; }
    public UpdateProductRequestDto Dto { get; }
    public UpdateProductCommand(Guid id, UpdateProductRequestDto dto)
    {
        Id = id;
        Dto = dto;
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
        var product = new ProductEntity(request.Id, request.Dto.Name, request.Dto.Price);
        return _storage.UpdateAsync(product);
    }
}
