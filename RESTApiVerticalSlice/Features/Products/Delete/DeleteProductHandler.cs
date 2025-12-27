using MediatR;

using RESTApiVerticalSlice.Storage;

namespace RESTApiVerticalSlice.Features.Products.Delete;

public sealed class DeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; }
    public DeleteProductCommand(Guid id) => Id = id;
}

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly InMemoryProductStorage _storage;
    public DeleteProductHandler(InMemoryProductStorage storage)
    {
        _storage = storage;
    }

    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken) => _storage.DeleteAsync(request.Id);
}
