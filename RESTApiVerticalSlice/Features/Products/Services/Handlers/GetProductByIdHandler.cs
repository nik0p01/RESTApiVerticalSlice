using MediatR;
using RESTApiVerticalSlice.Features.Products.Data;
using RESTApiVerticalSlice.Features.Products.Models;

namespace RESTApiVerticalSlice.Features.Products.Services.Handlers;

public sealed class GetProductByIdQuery : IRequest<Product?>
{
    public Guid Id { get; }
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _repository;
    public GetProductByIdHandler(IProductRepository repo)
    {
        _repository = repo;
    }

    public Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) => _repository.GetByIdAsync(request.Id);
}
