using MediatR;
using RESTApiVerticalSlice.Features.Products.Data;

namespace RESTApiVerticalSlice.Features.Products.Services.Handlers;

public sealed class DeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; }
    public DeleteProductCommand(Guid id) => Id = id;
}

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;
    public DeleteProductHandler(IProductRepository repo)
    {
        _repository = repo;
    }

    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken) => _repository.DeleteAsync(request.Id);
}
